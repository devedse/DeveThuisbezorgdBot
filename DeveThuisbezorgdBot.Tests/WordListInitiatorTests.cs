using Xunit;

namespace DeveThuisbezorgdBot.Tests
{
    public class WordListInitiatorTests
    {
        [Fact]
        public void WordListsAreInitializedCorrectly()
        {
            //Arrange

            //Act
            var wordlists = WordListInitiator.WordLists;

            //Assert
            Assert.NotEmpty(wordlists);
        }
    }
}
