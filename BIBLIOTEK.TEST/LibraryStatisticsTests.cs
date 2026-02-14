using BIBLIOTEK;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class LibraryStatisticsTests
    {
        [Fact]
        public void GetTotalBooks_ShouldReturnCorrectCount()
        {
            var catalog = new BookCatalog();
            catalog.AddBook(new Book("1", "A", "A", 2000));
            catalog.AddBook(new Book("2", "B", "B", 2001));

            var count = catalog.GetTotalBookCount();

            Assert.Equal(2, count);
        }

        [Fact]
        public void GetActiveLoanCount_ShouldReturnCorrectCount()
        {
            var loanManager = new LoanManager();
            var book = new Book("1", "Titel", "Förf", 2020);
            var member = new Member("M1", "Namn", "Email");
            
            loanManager.CreateLoan(book, member);
            var count = loanManager.GetTotalLoanCount();

            Assert.Equal(1, count);
        }
        
        [Fact]
        public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
        {
            var catalog = new BookCatalog();
            catalog.AddBook(new Book("1", "Zebra", "A", 2000));
            catalog.AddBook(new Book("2", "Apa", "B", 2001));

            var sortedBooks = catalog.SortBooksByTitle();

            Assert.Equal("Apa", sortedBooks[0].Title);
            Assert.Equal("Zebra", sortedBooks[1].Title);
        }
    }
}
