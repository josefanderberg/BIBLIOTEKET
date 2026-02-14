using BIBLIOTEK;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class LibraryStatisticsTests
    {
        [Fact]

        public void GetTotalBooks_ShouldReturnCorrectCount()
        {
            // Arrange
            var catalog = new BookCatalog();
            catalog.AddBook(new Book("1", "A", "A", 2000));
            catalog.AddBook(new Book("2", "B", "B", 2001));

            // Act
            var count = catalog.GetTotalBookCount();

            // Assert
            Assert.Equal(2, count);
        }

        [Fact]
        public void GetActiveLoanCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var loanManager = new LoanManager();
            var book = new Book("1", "Titel", "Förf", 2020);
            var member = new Member("M1", "Namn", "Email");
            
            // Act
            loanManager.CreateLoan(book, member);
            var count = loanManager.GetActiveLoanCount();

            // Assert
            Assert.Equal(1, count);
        }
        
        [Fact]
        public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
        {
            // Arrange
            var catalog = new BookCatalog();
            catalog.AddBook(new Book("1", "Zebra", "A", 2000));
            catalog.AddBook(new Book("2", "Apa", "B", 2001));

            // Act
            var sortedBooks = catalog.SortBooksByTitle();

            // Assert
            Assert.Equal("Apa", sortedBooks[0].Title);
            Assert.Equal("Zebra", sortedBooks[1].Title);
        }
    }
}
