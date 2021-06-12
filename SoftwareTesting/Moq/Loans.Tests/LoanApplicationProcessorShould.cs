using System;
using Loans.Domain.Applications;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace Loans.Tests
{
    [TestFixture]
    public class LoanApplicationProcessorShould
    {
        [Test]
        public void DeclineLowSalary()
        {
            var product = new LoanProduct(99, "Loan", 5.25m);
            var amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "Dzorapi 70/3",
                64999); // min salary is 65000 => should be declined

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            var mockCreditScorer = new Mock<ICreditScorer>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.False);
        }

        delegate void ValidateCallback(string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status);

        [Test]
        public void Accept()
        {
            var product = new LoanProduct(99, "Loan", 5.25m);
            var amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "Dzorapi 70/3",
                65000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            // if IIdentityVerifier calls
            //mockIdentityVerifier.Setup(x => x.Validate("A", 25, "Dzorapi 70/3")).Returns(true); // if we change the name the test will fail
            mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "Dzorapi 70/3")).Returns(true);
            //mockIdentityVerifier.Setup(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true); // will validate regardless of what argument is passed

            //var isValidOutValue = true; // should declare out parameter beforehand
            //mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "Dzorapi 70/3", out isValidOutValue));

            //mockIdentityVerifier.Setup(x=>x.Validate("Sarah", 25, "Dzorapi 70/3", ref It.Ref<IdentityVerificationStatus>.IsAny)).Callback(new ValidateCallback((string applicantName, int applicantAge, string applicantAddress, ref IdentityVerificationStatus status)=>status = new IdentityVerificationStatus(true)));

            //var mockCreditScorer = new Mock<ICreditScorer>();
            var mockCreditScorer = new Mock<ICreditScorer>() { DefaultValue = DefaultValue.Mock }; // automatically populate property hierarchy with mock objects; Empty will set default values
            mockCreditScorer.SetupAllProperties(); // make sure to do it before any specific setups

            //mockCreditScorer.Setup(x => x.Score).Returns(400);
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);

            //mockCreditScorer.SetupProperty(x => x.Count); // track changes on Count prop

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            mockCreditScorer.VerifyGet(x => x.ScoreResult.ScoreValue.Score, Times.Once); // verify that Score property getter was invoked
            mockCreditScorer.VerifySet(x => x.Count = It.IsAny<int>(), Times.Once);

            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockCreditScorer.Object.Count, Is.EqualTo(1));
        }

        [Test]
        public void NullReturnExample()
        {
            var mock = new Mock<INullExample>();
            mock.Setup(x => x.SomeMethod())/*.Returns<string>(null)*/; // mock objects by default return null

            var mockReturnValue = mock.Object.SomeMethod();

            Assert.That(mockReturnValue, Is.Null);
        }

        [Test]
        public void InitializeIdentityVerifier()
        {
            var product = new LoanProduct(99, "Loan", 5.25m);
            var amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "Dzorapi 70/3",
                65000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "Dzorapi 70/3")).Returns(true);
            var mockCreditScorer = new Mock<ICreditScorer> { DefaultValue = DefaultValue.Mock };
            mockCreditScorer.SetupAllProperties();

            //mockCreditScorer.Setup(x => x.Score).Returns(400);
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            mockIdentityVerifier.Verify(x => x.Initialize()); // if you remove _identityVerifier.Initialize(); line from LoanApplicationProcessor.cs, this line will throw an exception, saying that Initialize was not called

            mockIdentityVerifier.Verify(x => x.Validate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()));

            mockCreditScorer.Verify(x => x.CalculateScore("Sarah", "Dzorapi 70/3"), Times.Once);

            mockIdentityVerifier.VerifyNoOtherCalls(); // verify that no other calls were made except of those verified
        }

        [Test]
        public void DeclineWhenCreditScorerThrows()
        {
            var product = new LoanProduct(99, "Loan", 5.25m);
            var amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "Dzorapi 70/3",
                65000);

            var mockIdentityVerifier = new Mock<IIdentityVerifier>();
            mockIdentityVerifier.Setup(x => x.Validate("Sarah", 25, "Dzorapi 70/3")).Returns(true);

            var mockCreditScorer = new Mock<ICreditScorer>(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);
            mockCreditScorer.Setup(x => x.CalculateScore(It.IsAny<string>(), It.IsAny<string>())).Throws<InvalidOperationException>();

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.False);
        }

        [Test]
        public void AcceptUsingPartialMock()
        {
            var product = new LoanProduct(99, "Loan", 5.25m);
            var amount = new LoanAmount("USD", 200_000);
            var application = new LoanApplication(42,
                product,
                amount,
                "Sarah",
                25,
                "Dzorapi 70/3",
                65000);

            var mockIdentityVerifier = new Mock<IdentityVerifierServiceGateway>();
            //mockIdentityVerifier.Protected().Setup<bool>("CallService", "Sarah", 25, "Dzorapi 70/3").Returns(true);
            mockIdentityVerifier.Protected().As<IIdentityVerifierServiceGatewayProtectedMembers>()
                .Setup(x => x.CallService(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            var expectedTime = new DateTime(2000, 2, 2);
            //mockIdentityVerifier.Protected().Setup<DateTime>("GetCurrentTime").Returns(expectedTime);
            mockIdentityVerifier.Protected().As<IIdentityVerifierServiceGatewayProtectedMembers>()
                .Setup(x => x.GetCurrentTime()).Returns(expectedTime);

            var mockCreditScorer = new Mock<ICreditScorer>();
            mockCreditScorer.Setup(x => x.ScoreResult.ScoreValue.Score).Returns(300);

            var sut = new LoanApplicationProcessor(mockIdentityVerifier.Object, mockCreditScorer.Object);

            sut.Process(application);

            Assert.That(application.GetIsAccepted(), Is.True);
            Assert.That(mockIdentityVerifier.Object.LastCheckTime, Is.EqualTo(expectedTime));
        }
    }

    public interface INullExample
    {
        string SomeMethod();
    }

    public interface IIdentityVerifierServiceGatewayProtectedMembers
    {
        bool CallService(string applicantName, int applicantAge, string applicantAddress);
        DateTime GetCurrentTime();
    }
}