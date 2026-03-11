namespace BIBLIOTEK.Core;

public class Loan
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public Book Book { get; set; }

    public int MemberId { get; set; }
    public Member Member { get; set; }

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
    // Beräknad property
    public bool IsReturned => ReturnDate.HasValue;
    
    // Beräknad property
    public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;

    // Parameterlös konstruktor krävs av Entity Framework
    public Loan() { }

    public Loan(Book book, Member member, DateTime loanDate, DateTime dueDate)
    {
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
        return $"Loan ID: {Id}, Book: {Book.Title}, Member: {Member.Name}, Due: {DueDate.ToShortDateString()}, Returned: {IsReturned}";
    }       
}   