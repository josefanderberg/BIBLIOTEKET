using BIBLIOTEK; // Need to verify namespace
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var isbn = "978-91-0-012345-6";
            var title = "Testbok";
            var author = "Testförfattare";
            var year = 2024;

            // Act
            var book = new Book(isbn, title, author, year);

            // Assert
            Assert.Equal(isbn, book.ISBN);
            Assert.Equal(title, book.Title);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange
            var book = new Book("123", "Title", "Author", 2024);

            // Act
            var isAvailable = book.IsAvailable;

            // Assert
            Assert.True(isAvailable);
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
