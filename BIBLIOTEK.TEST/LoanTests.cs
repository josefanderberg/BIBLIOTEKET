using System;
using BIBLIOTEK;
using Xunit;

namespace BIBLIOTEK.TEST
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan("L1", book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act
            var isOverdue = loan.IsOverdue;

            // Assert
            Assert.False(isOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan("L2", book, member, DateTime.Now.AddDays(-20), DateTime.Now.AddDays(-1));

            // Act
            var isOverdue = loan.IsOverdue;

            // Assert
            Assert.True(isOverdue);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");
            var loan = new Loan("L3", book, member, DateTime.Now, DateTime.Now.AddDays(14));

            // Act
            loan.ReturnBook();

            // Assert
            Assert.True(loan.IsReturned);
            Assert.NotNull(loan.ReturnDate);
        }
    }
}
