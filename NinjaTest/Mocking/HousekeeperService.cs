using System.Net;
using System.Net.Mail;
using System.Text;

namespace NinjaTest.Mocking
{
    public class HousekeeperService
    {
        private readonly IHouseKeeperRepository _houseKeeperRepository;
        private readonly IStatementGenerator _statementGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IXtraMessageBox _xtraMessageBox;

        public HousekeeperService(IHouseKeeperRepository houseKeeperRepository, IStatementGenerator statementGenerator,
            IEmailSender emailSender, IXtraMessageBox xtraMessageBox)
        {
            _houseKeeperRepository = houseKeeperRepository;
            _statementGenerator = statementGenerator;
            _emailSender = emailSender;
            _xtraMessageBox = xtraMessageBox;
        }

        public async Task  SendStatementEmails(DateTime statementDate)
        {
             List<Housekeeper> housekeepers = await _houseKeeperRepository.GetHousekeepers();

             foreach (var housekeeper in housekeepers)
             {
                 if (string.IsNullOrWhiteSpace(housekeeper.Email))
                     continue;

                 var statementFilename = _statementGenerator.SaveStatement(housekeeper.Oid, housekeeper.FullName, statementDate);

                 if (string.IsNullOrWhiteSpace(statementFilename))
                     continue;

                 var emailAddress = housekeeper.Email;
                 var emailBody = housekeeper.StatementEmailBody;

                 try
                 {
                     _emailSender.EmailFile(emailAddress, emailBody, statementFilename,
                         $"Sandpiper Statement {statementDate:yyyy-MM} {housekeeper.FullName}");
                 }
                 catch (Exception e)
                 {
                     _xtraMessageBox.Show(e.Message, $"Email failure: {emailAddress}",
                         MessageBoxButtons.OK);
                 }
             }
        }
    }

    public interface IHouseKeeperRepository
    {
        public Task<List<Housekeeper>> GetHousekeepers();
    }

    public enum MessageBoxButtons
    {
        OK
    }

    public interface IXtraMessageBox
    {
        public void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
    }

    public class MainForm
    {
        public bool HousekeeperStatementsSending { get; set; }
    }

    public class DateForm
    {
        public DateForm(string statementDate, object endOfLastMonth)
        {
        }

        public DateTime Date { get; set; }

        public DialogResult ShowDialog()
        {
            return DialogResult.Abort;
        }
    }

    public enum DialogResult
    {
        Abort,
        OK
    }

    public class SystemSettingsHelper
    {
        public static string EmailSmtpHost { get; set; }
        public static int EmailPort { get; set; }
        public static string EmailUsername { get; set; }
        public static string EmailPassword { get; set; }
        public static string EmailFromEmail { get; set; }
        public static string EmailFromName { get; set; }
    }

    public class Housekeeper
    {
        public string? Email { get; set; }
        public int Oid { get; set; }
        public string FullName { get; set; }
        public string StatementEmailBody { get; set; }
    }

    public class HousekeeperStatementReport
    {
        public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
        {
        }

        public bool HasData { get; set; }

        public void CreateDocument()
        {
        }

        public void ExportToPdf(string filename)
        {
        }
    }
}