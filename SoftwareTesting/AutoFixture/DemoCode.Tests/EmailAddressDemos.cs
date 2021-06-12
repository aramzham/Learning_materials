using System.Net.Mail;
using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class EmailAddressDemos
    {
        [Fact]
        public void Email()
        {
            // arrange
            var fixture = new Fixture();

            //var localPart = fixture.Create<EmailAddressLocalPart>().LocalPart;
            //var domain = fixture.Create<DomainName>().Domain;
            //var fullAddress = $"{localPart}@{domain}";

            //var sut = new EmailMessage(fullAddress, fixture.Create<string>(), fixture.Create<bool>());
            var sut = new EmailMessage(fixture.Create<MailAddress>().Address, fixture.Create<string>(), fixture.Create<bool>());
        }
    }
}