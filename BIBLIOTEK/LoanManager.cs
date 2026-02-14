public class LoanManager
{
    private List<Loan> loans = new List<Loan>();

    public void CreateLoan(Book book, Member member)
    {
        if (!book.IsAvailable)
        {
            throw new InvalidOperationException($"The book '{book.Title}' is not available.");
        }

        // Create a new loan
        var loan = new Loan(Guid.NewGuid().ToString().Substring(0, 8), book, member, DateTime.Now, DateTime.Now.AddDays(15));
        
        loans.Add(loan);
        
        // Update book status
        book.IsAvailable = false;
        
        // Update member record
        member.BorrowBook(book);
    }

    public void ReturnLoan(Book book, Member member)
    {
        var loan = loans.FirstOrDefault(l => l.Book == book && l.Member == member && !l.IsReturned);
        
        if (loan == null)
        {
            throw new ArgumentException("No active loan found for this book and member.");
        }

        loan.ReturnBook(); 
    }

    public List<Loan> GetAllLoans()
    {
        return new List<Loan>(loans);
    }

    public int GetTotalLoanCount()
    {
        return loans.Count;
    }   

}
