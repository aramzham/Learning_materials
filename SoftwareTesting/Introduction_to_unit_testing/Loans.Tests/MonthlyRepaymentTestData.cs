using System.Collections;
using NUnit.Framework;

namespace Loans.Tests
{
    public class MonthlyRepaymentTestData
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(200000m, 6.5m, 30, 1264.14m);
                yield return new TestCaseData(200000m, 10m, 30, 4387.86m);
            }
        }
    }
}