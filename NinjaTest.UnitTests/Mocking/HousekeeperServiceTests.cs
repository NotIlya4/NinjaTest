using Moq;
using NinjaTest.Mocking;

namespace NinjaTest.Test.Mocking;

public class HousekeeperServiceTests
{
    private Mock<IHouseKeeperRepository> _housekeeperRepository = null!;
    private Mock<IStatementGenerator> _statementGenerator = null!;
    private Mock<IEmailSender> _emailSender = null!;
    private Mock<IXtraMessageBox> _xtraMessageBox = null!;
    private HousekeeperService _housekeeperService = null!;
    private DateTime _statementDate;

    private Housekeeper _housekeeper = null!;
    private string? _statementGeneratorReturn;

    [SetUp]
    public void SetUp()
    {
        _housekeeper = new Housekeeper()
        {
            Email = "a",
            FullName = "b",
            Oid = 1,
            StatementEmailBody = "c"
        };
        _statementGeneratorReturn = "filename";
        _statementDate = new DateTime(2017, 1, 1);

        _emailSender = new();

        _housekeeperRepository = new();
        _housekeeperRepository.Setup(hr => hr.GetHousekeepers()).ReturnsAsync(new List<Housekeeper>()
        {
            _housekeeper
        });

        _statementGenerator = new();
        _statementGenerator
            .Setup(sg => 
                sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
            .Returns(() => _statementGeneratorReturn);

        _xtraMessageBox = new();
        
        _housekeeperService = new(_housekeeperRepository.Object, _statementGenerator.Object, _emailSender.Object, _xtraMessageBox.Object);
    }

    [Test]
    public async Task SendStatementEmails_ValidEmail_GenerateStatement()
    {
        await _housekeeperService.SendStatementEmails(_statementDate);
        
        _statementGenerator.Verify(sg => 
            sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Once);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public async Task SendStatementEmails_InvalidEmail_DoNotGenerateStatement(string? email)
    {
        _housekeeper.Email = email;
        
        await _housekeeperService.SendStatementEmails(_statementDate);
        
        _statementGenerator.Verify(sg => 
            sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
    }

    [Test]
    public async Task SendStatementEmails_ValidFileStatement_SendEmail()
    {
        await _housekeeperService.SendStatementEmails(_statementDate);
        
        VerifyEmailSend();
    }
    
    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public async Task SendStatementEmails_InvalidFileStatement_DoNotSendEmail(string? fileStatement)
    {
        _statementGeneratorReturn = fileStatement;
        
        await _housekeeperService.SendStatementEmails(_statementDate);
        
        VerifyEmailNotSend();
    }

    private void VerifyEmailSend()
    {
        _emailSender
            .Verify(es => 
                es.EmailFile(
                    _housekeeper.Email!, 
                    _housekeeper.StatementEmailBody, 
                    _statementGeneratorReturn!,
                    It.IsAny<string>()), Times.Once);
    }
    
    private void VerifyEmailNotSend()
    {
        _emailSender
            .Verify(es => 
                es.EmailFile(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>(),
                    It.IsAny<string>()), Times.Never);
    }
}