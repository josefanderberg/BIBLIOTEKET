using BIBLIOTEK.Core;
using BIBLIOTEK.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class MemberRepositoryTests
    {
        private LibraryContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new LibraryContext(options);
        }

        [Fact]
        public async Task AddMemberAsync_ShouldSaveMemberToDatabase()
        {
            // Arrange
            using var context = CreateContext("AddMemberTest");
            var repo = new MemberRepository(context);
            var member = new Member("M123", "Test Person", "test@person.com");

            // Act
            await repo.AddMemberAsync(member);

            // Assert
            var savedMember = await context.Members.FirstOrDefaultAsync(m => m.MemberId == "M123");
            Assert.NotNull(savedMember);
            Assert.Equal("Test Person", savedMember.Name);
        }

        [Fact]
        public async Task GetAllMembersAsync_ShouldReturnAllMembers()
        {
            // Arrange
            using var context = CreateContext("GetAllMembersTest");
            context.Members.AddRange(
                new Member("M1", "Anna", "anna@test.com"),
                new Member("M2", "Bertil", "bertil@test.com")
            );
            await context.SaveChangesAsync();
            var repo = new MemberRepository(context);

            // Act
            var members = await repo.GetAllMembersAsync();

            // Assert
            Assert.Equal(2, members.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMemberWithLoans()
        {
            // Arrange
            using var context = CreateContext("GetByIdMemberTest");
            var member = new Member("M3", "Cecilia", "cecilia@test.com");
            var book = new Book("978-1", "Bok", "Författare", 2020);
            context.Members.Add(member);
            context.Books.Add(book);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, DateTime.Now.AddDays(14));
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            var repo = new MemberRepository(context);

            // Act
            var result = await repo.GetByIdAsync(member.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result!.Loans);
            Assert.Equal("Bok", result.Loans.First().Book.Title);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingMembers()
        {
            // Arrange
            using var context = CreateContext("SearchMemberTest");
            context.Members.AddRange(
                new Member("M4", "David", "david@test.com"),
                new Member("M5", "Erik", "erik@test.com")
            );
            await context.SaveChangesAsync();
            var repo = new MemberRepository(context);

            // Act
            var results = await repo.SearchAsync("david");

            // Assert
            Assert.Single(results);
            Assert.Equal("David", results.First().Name);
        }
    }
}
