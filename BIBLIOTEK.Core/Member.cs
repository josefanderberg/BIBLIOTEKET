namespace BIBLIOTEK.Core;

public class Member : ISearchable
{
    public int Id { get; set; }
    public string MemberId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime MemberSince { get; set; }    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();


    private List<Book> borrowedBooks = new List<Book>();

    // Parameterlös konstruktor krävs av Entity Framework
    public Member() { }

    public Member(string memberId, string name, string email)
    {
        MemberId = memberId;
        Name = name;
        Email = email;
        MemberSince = DateTime.Now;
    }

    public void BorrowBook(Book book)
    {
        borrowedBooks.Add(book);
    }

    public void ReturnBook(Book book)
    {
        borrowedBooks.Remove(book);
    }

    public IReadOnlyList<Book> BorrowedBooks => borrowedBooks.AsReadOnly();

    public string GetInfo()
    {
        return $"{Name} (ID: {MemberId}) - E-post: {Email}";
    }       

    public bool Matches(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return false;
        searchTerm = searchTerm.ToLower();
        return Name.ToLower().Contains(searchTerm) || 
               Email.ToLower().Contains(searchTerm) || 
               MemberId.ToLower().Contains(searchTerm);
    }
}