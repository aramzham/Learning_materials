using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    //[TestFixture, Category("product comparison")]
    [TestFixture, ProductComparison]
    public class MonthlyRepaymentComparisonShould
    {
        [Test, Category("abc")]
        public void RespectValueEquality()
        {
            var a = new MonthlyRepaymentComparison("a", 32.32m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 32.32m, 22.22m);

            Assert.That(a, Is.EqualTo(b));
        }

        [Test, Category("abc")]
        public void RespectValueInequality()
        {
            var a = new MonthlyRepaymentComparison("a", 32.32m, 22.22m);
            var b = new MonthlyRepaymentComparison("a", 32.32m, 22.22m);

            Assert.That(a, Is.Not.EqualTo(b));
        }
    }
}