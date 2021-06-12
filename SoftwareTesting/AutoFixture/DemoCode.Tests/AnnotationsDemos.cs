using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class AnnotationsDemos
    {
        [Fact]
        public void BasicString()
        {
            // arrange
            var fixture = new Fixture();

            var player = fixture.Create<PlayerCharacter>(); // AutoFixture respects some data annotations
        }
    }
}