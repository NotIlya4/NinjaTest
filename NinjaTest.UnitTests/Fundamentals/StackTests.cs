namespace NinjaTest.Test.Fundamentals;

public class StackTests
{
    private NinjaTest.Fundamentals.Stack<int> _emptyStack = null!;
    private NinjaTest.Fundamentals.Stack<int> _oneTwoThreeStack = null!;

    [SetUp]
    public void SetUp()
    {
        _emptyStack = new();
        
        _oneTwoThreeStack = new();
        _oneTwoThreeStack.Push(1);
        _oneTwoThreeStack.Push(2);
        _oneTwoThreeStack.Push(3);
    }
    
    [Test]
    public void Push_Value_CountIncreases()
    {
        _emptyStack.Push(1);
        _emptyStack.Push(2);
        
        Assert.That(_emptyStack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Count_EmptyStack_ReturnZero()
    {
        Assert.That(_emptyStack.Count, Is.EqualTo(0));
    }

    [Test]
    public void Pop_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => { _emptyStack.Pop(); }, Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_NotEmptyStack_ReturnObjectFromTop()
    {
        int result = _oneTwoThreeStack.Pop();
        
        Assert.That(result, Is.EqualTo(3));
    }
    
    [Test]
    public void Pop_NotEmptyStack_RemoveObjectFromTop()
    {
        int result = _oneTwoThreeStack.Pop();
        
        Assert.That(_oneTwoThreeStack.Count, Is.EqualTo(2));
    }

    [Test]
    public void Peek_EmptyStack_ThrowsInvalidOperationException()
    {
        Assert.That(() => { _emptyStack.Peek(); }, Throws.InvalidOperationException);
    }
    
    [Test]
    public void Peek_NotEmptyStack_ReturnObjectFromTop()
    {
        int result = _oneTwoThreeStack.Peek();
        
        Assert.That(result, Is.EqualTo(3));
    }
    
    [Test]
    public void Peek_NotEmptyStack_NotRemoveObjectFromTop()
    {
        int result = _oneTwoThreeStack.Peek();
        
        Assert.That(_oneTwoThreeStack.Count, Is.EqualTo(3));
    }
}