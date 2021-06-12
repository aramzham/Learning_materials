using System;
using AutoFixture;
using Xunit;

namespace DemoCode.Tests
{
    public class SequenceDemos
    {
        [Fact]
        public void SequenceOfStrings()
        {
            // arrange
            var fixture = new Fixture();

            var messages = fixture.CreateMany<string>();
            var ints = fixture.CreateMany<int>(6);
        }

        [Fact]
        public void AddingToLists()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new DebugMessageBuffer();
            var numberOfMessages = fixture.Create<int>();
            fixture.AddManyTo(sut.Messages, numberOfMessages);

            // act
            sut.WriteMessages();

            // assert
            Assert.Equal(numberOfMessages, sut.MessagesWritten);
        }

        [Fact]
        public void AddingToListWithCreatorFunction()
        {
            // arrange
            var fixture = new Fixture();
            var sut = new DebugMessageBuffer();
            var rnd = new Random();

            fixture.AddManyTo(sut.Messages, () => rnd.Next().ToString());
        }
    }
}