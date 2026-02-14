using BIBLIOTEK;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class SearchTests
    {
        [Theory]
        [InlineData("Tolkien", true)]
        [InlineData("tolkien", true)]  // Case-insensitive check
        [InlineData("Rowling", false)]
        public void Book_Matches_ShouldFindByAuthor(string searchTerm, bool expected)
        {
            // Arrange
            var book = new Book("123", "Sagan om ringen", "J.R.R. Tolkien", 1954);

            // Act
            var result = book.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("Anna", true)]
        [InlineData("anna@example.com", true)]
        [InlineData("Bertil", false)]
        public void Member_Matches_ShouldFindByNameOrEmail(string searchTerm, bool expected)
        {
            // Arrange
            var member = new Member("M1", "Anna Andersson", "anna@example.com");

            // Act
            var result = member.Matches(searchTerm);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
