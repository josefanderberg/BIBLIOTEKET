using Bunit;
using BIBLIOTEK.Core;
using BIBLIOTEK.Web.Shared;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class BookCardTests : TestContext
    {
        [Fact]
        public void BookCard_ShouldDisplayCorrectInfo()
        {
            // Arrange
            var book = new Book("123", "Test Titel", "Test Författare", 2024);
            book.IsAvailable = true;

            // Act
            var cut = RenderComponent<BookCard>(parameters => parameters
                .Add(p => p.Book, book)
            );

            // Assert
            cut.Find("h5").MarkupMatches("<h5 class=\"card-title\">Test Titel</h5>");
            cut.Find("p").MarkupMatches("<p class=\"card-text\">Test Författare (2024)</p>");
            Assert.Contains("Tillgänglig", cut.Markup);
        }

        [Fact]
        public void BookCard_ShouldShowBorrowedStatus()
        {
            // Arrange
            var book = new Book("456", "Utlånad Bok", "Författare", 2023);
            book.IsAvailable = false;

            // Act
            var cut = RenderComponent<BookCard>(parameters => parameters
                .Add(p => p.Book, book)
            );

            // Assert
            Assert.Contains("Utlånad", cut.Markup);
            var badge = cut.Find(".badge");
            Assert.Contains("bg-danger", badge.ClassName);
        }
    }
}
