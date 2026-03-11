using BIBLIOTEK.Core;
using BIBLIOTEK.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class BookRepositoryTests
    {
        private LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddBookAsync_ShouldSaveBookToDatabase()
        {
            // Arrange
            using var context = CreateContext("AddBookTest");
            var repo = new BookRepository(context);
            var book = new Book("978-0-00-000001-1", "Testbok", "Testförfattare", 2024);

            // Act
            await repo.AddBookAsync(book);

            // Assert
            var savedBook = await context.Books.FirstOrDefaultAsync(b => b.ISBN == "978-0-00-000001-1");
            Assert.NotNull(savedBook);
            Assert.Equal("Testbok", savedBook.Title);
        }

        [Fact]
        public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        {
            // Arrange
            using var context = CreateContext("GetAllBooksTest");
            context.Books.AddRange(
                new Book("111", "Bok 1", "Författare 1", 2020),
                new Book("222", "Bok 2", "Författare 2", 2021),
                new Book("333", "Bok 3", "Författare 3", 2022)
            );
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            var books = await repo.GetAllBooksAsync();

            // Assert
            Assert.Equal(3, books.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectBook()
        {
            // Arrange
            using var context = CreateContext("GetByIdTest");
            var book = new Book("444", "Specifik Bok", "Författare", 2023);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            var result = await repo.GetByIdAsync(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Specifik Bok", result!.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenBookNotFound()
        {
            // Arrange
            using var context = CreateContext("GetByIdNotFoundTest");
            var repo = new BookRepository(context);

            // Act
            var result = await repo.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBookByIsbnAsync_ShouldReturnCorrectBook()
        {
            // Arrange
            using var context = CreateContext("GetByIsbnTest");
            context.Books.Add(new Book("978-UNIQUE", "ISBN Bok", "Författare", 2024));
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            var result = await repo.GetBookByIsbnAsync("978-UNIQUE");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ISBN Bok", result!.Title);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldModifyBook()
        {
            // Arrange
            using var context = CreateContext("UpdateBookTest");
            var book = new Book("555", "Gammal Titel", "Författare", 2020);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            book.Title = "Ny Titel";
            await repo.UpdateBookAsync(book);

            // Assert
            var updated = await context.Books.FindAsync(book.Id);
            Assert.Equal("Ny Titel", updated!.Title);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldRemoveBook()
        {
            // Arrange
            using var context = CreateContext("DeleteBookTest");
            var book = new Book("666", "Ta Bort Mig", "Författare", 2020);
            context.Books.Add(book);
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            await repo.DeleteBookAsync(book.Id);

            // Assert
            var deleted = await context.Books.FindAsync(book.Id);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByTitle()
        {
            // Arrange
            using var context = CreateContext("SearchTitleTest");
            context.Books.AddRange(
                new Book("101", "Harry Potter", "J.K. Rowling", 1997),
                new Book("102", "Clean Code", "Robert Martin", 2008),
                new Book("103", "Sagan om ringen", "Tolkien", 1954)
            );
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            var results = await repo.SearchAsync("harry");

            // Assert
            Assert.Single(results);
            Assert.Equal("Harry Potter", results.First().Title);
        }

        [Fact]
        public async Task SearchAsync_ShouldFindBooksByAuthor()
        {
            // Arrange
            using var context = CreateContext("SearchAuthorTest");
            context.Books.AddRange(
                new Book("201", "Bok A", "Astrid Lindgren", 1950),
                new Book("202", "Bok B", "Annan Författare", 2000)
            );
            await context.SaveChangesAsync();
            var repo = new BookRepository(context);

            // Act
            var results = await repo.SearchAsync("lindgren");

            // Assert
            Assert.Single(results);
            Assert.Equal("Bok A", results.First().Title);
        }

        [Fact]
        public async Task LoanIntegration_ShouldUpdateBookAvailability()
        {
            // Arrange - Testa att lån och boktillgänglighet fungerar tillsammans
            using var context = CreateContext("LoanIntegrationTest");
            var book = new Book("999", "Integrationsbok", "Författare", 2024);
            var member = new Member("M099", "Test Person", "test@test.com");
            context.Books.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Act - Skapa ett lån
            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            book.IsAvailable = false;
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Assert
            var savedBook = await context.Books.FindAsync(book.Id);
            Assert.False(savedBook!.IsAvailable);

            var savedLoan = await context.Loans.FirstOrDefaultAsync(l => l.BookId == book.Id);
            Assert.NotNull(savedLoan);
            Assert.False(savedLoan!.IsReturned);
        }
    }
}
