public class Loan
{
    public string LoanId { get; }
    public Book Book { get; set; }
    public Member Member { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; private set; }
    
    // Beräknad property
    public bool IsReturned => ReturnDate.HasValue;
    
    // Beräknad property
    public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;

    public Loan(string loanId, Book book, Member member, DateTime loanDate, DateTime dueDate)
    {
        LoanId = loanId;
        Book = book;
        Member = member;
        LoanDate = loanDate;
        DueDate = dueDate;
        ReturnDate = null;
    }

    public void ReturnBook()
    {
        ReturnDate = DateTime.Now;
        // Update the book status
        Book.IsAvailable = true; 
        // Update the member status
        Member.ReturnBook(Book);
    }

    public string GetInfo()
    {
        return $"Loan ID: {LoanId}, Book: {Book.Title}, Member: {Member.Name}, Due: {DueDate.ToShortDateString()}, Returned: {IsReturned}";
    }       
}   