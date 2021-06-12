using NUnit.Framework;
using Loans.Domain.Applications;
using System;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanRepaymentCalculatorShould
    {
        [Test]
        [TestCase(200000, 6.5, 30, 1264.14)]
        [TestCase(200000, 10, 30, 1755.14)]
        [TestCase(500000, 10, 30, 4387.86)]
        public void CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        [TestCase(200000, 6.5, 30, ExpectedResult = 1264.14)]
        [TestCase(200000, 10, 30, ExpectedResult = 1755.14)]
        [TestCase(500000, 10, 30, ExpectedResult = 4387.86)]
        public decimal CalculateCorrectMonthlyRepayment(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test, TestCaseSource(typeof(MonthlyRepaymentTestData), nameof(MonthlyRepaymentTestData.TestCases))]
        public void CalculateCorrectMonthlyRepayment_Centralized(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            Assert.That(sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears)), Is.EqualTo(expectedMonthlyPayment));
        }

        [Test, TestCaseSource(typeof(MonthlyRepaymentTestDataWithReturn), nameof(MonthlyRepaymentTestDataWithReturn.TestCases))]
        public decimal CalculateCorrectMonthlyRepayment_CentralizedWithReturn(decimal principal, decimal interestRate, int termInYears)
        {
            var sut = new LoanRepaymentCalculator();

            return sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test, TestCaseSource(typeof(MonthlyRepaymentCsvData), nameof(MonthlyRepaymentCsvData.GetTestCases), new object[] { "Data.csv" })]
        public void CalculateCorrectMonthlyRepayment_Csv(decimal principal, decimal interestRate, int termInYears, decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Combinatorial([Values(100000, 200000, 500000)]decimal principal, 
                                                                   [Values(6.5, 10,20)] decimal interestRate, 
                                                                   [Values(10,20,30)]int termInYears)
        {
            // combines each value with another => we'll get 27 tests
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }

        [Test, Sequential]
        public void CalculateCorrectMonthlyRepayment_Sequential([Values(200000, 200000, 500000)] decimal principal, 
                                                                [Values(6.5, 10, 20)] decimal interestRate, 
                                                                [Values(10, 20, 30)] int termInYears, 
                                                                [Values(1264.14, 1755.14, 4387.86)] decimal expectedMonthlyPayment)
        {
            var sut = new LoanRepaymentCalculator();

            var monthlyPayment = sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));

            Assert.That(monthlyPayment, Is.EqualTo(expectedMonthlyPayment));
        }

        [Test]
        public void CalculateCorrectMonthlyRepayment_Range([Range(50000, 100000, 50_000)] decimal principal, 
                                                           [Range(0.5, 20.00, 0.5)] decimal interestRate,
                                                           [Values(10, 20, 30)] int termInYears)
        {
            //var sut = new LoanRepaymentCalculator();

            //sut.CalculateMonthlyRepayment(new LoanAmount("USD", principal), interestRate, new LoanTerm(termInYears));
        }
    }
}