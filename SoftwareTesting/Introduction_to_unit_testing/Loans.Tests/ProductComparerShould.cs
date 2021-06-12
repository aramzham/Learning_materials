using System.Collections.Generic;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class ProductComparerShould
    {
        private List<LoanProduct> _products;
        private ProductComparer _sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // runs once before all tests
            // we also assume that these fields won't be modified in any of the tests (isolation!!)
            _products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1), new LoanProduct(2, "b", 2), new LoanProduct(3, "c", 3)
            };

            _sut = new ProductComparer(new LoanAmount("USD", 200_000m), _products);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // run after last test execution (may dispose something here)
        }

        [SetUp]
        public void Setup()
        {
            // runs before each test execution
        }

        [TearDown]
        public void TearDown()
        {
            // runs after each test execution
        }

        //[Test, Category("product comparison")]
        [Test, ProductComparison]
        public void ReturnCorrectNumberOfComparisons()
        {
            var comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnComparisons()
        {
            var comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            var comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            var expectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);

            Assert.That(comparisons, Does.Contain(expectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProduct_WithPartialKnownExpectedValues()
        {
            var comparisons = _sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(1)
                                        .Property("ProductName").EqualTo("a")
                                        .And
                                        .Property("InterestRate").EqualTo(1)
                                        .And
                                        .Property("MonthlyRepayment").GreaterThan(0));

            //Assert.That(comparisons, Has.Exactly(1).Matches<MonthlyRepaymentComparison>(x => x.ProductName == "a" &&
            //                                                                                 x.InterestRate == 1 &&
            //                                                                                 x.MonthlyRepayment > 0));

            Assert.That(comparisons, Has.Exactly(1).Matches(new MonthlyRepaymentGreaterThanZeroConstraint("a", 1)));
        }
    }
}