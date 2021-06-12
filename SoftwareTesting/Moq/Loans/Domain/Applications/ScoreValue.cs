namespace Loans.Domain.Applications
{
    public class ScoreValue
    {
        public virtual int Score { get; } // remove the virtual and test will fail
    }
}