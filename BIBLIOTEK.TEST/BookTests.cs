using BIBLIOTEK; // Need to verify namespace
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange & Act
            var book = new Book("123", "Title", "Author", 2024);

            // Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            // Arrange
            var book = new Book("123", "Titel", "Författare", 2024);

            // Act
            var info = book.GetInfo();

            // Assert
            Assert.Contains("Titel", info);
            Assert.Contains("Författare", info);
            Assert.Contains("123", info);
        }
    }
}
