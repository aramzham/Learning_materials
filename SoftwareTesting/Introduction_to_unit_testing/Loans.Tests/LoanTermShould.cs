using System;
using Loans.Domain.Applications;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanTermShould
    {
        [Test, Ignore("need to complete some work")]
        public void ReturnTermInMonths()
        {
            // Arrange (sut = system under test)
            var sut = new LoanTerm(1);

            // Act
            var numberOfMonths = sut.ToMonths();

            // Assert
            Assert.That(numberOfMonths, Is.EqualTo(12), "some helpful message");
        }

        [Test]
        public void StoreYears()
        {
            var sut = new LoanTerm(1);

            Assert.That(sut.Years, Is.EqualTo(1));
        }

        [Test]
        public void RespectValueEquality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(1);

            Assert.That(a, Is.EqualTo(b)); // will check the equality of 2 objects which is overriden
        }

        [Test]
        public void RespectValueInequality()
        {
            var a = new LoanTerm(1);
            var b = new LoanTerm(2);

            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void ReferenceEqualityExample()
        {
            var a = new LoanTerm(1);
            var b = a;
            var c = new LoanTerm(1);
            Assert.That(a, Is.SameAs(b)); // is for reference equality
            Assert.That(a, Is.Not.SameAs(c));
        }

        [Test]
        public void TestDouble()
        {
            var a = 1.0 / 3.0;
            Assert.That(a, Is.EqualTo(0.33).Within(0.005)); // not exactly equal, but within certain tolerance 
            Assert.That(a, Is.EqualTo(0.33).Within(10).Percent);
        }

        [Test]
        public void NotAllowZeroYears()
        {
            // action of creating a LoanTerm with invalid argument should throw an exception
            Assert.That(()=>new LoanTerm(0), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}