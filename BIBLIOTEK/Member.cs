public class Member : ISearchable
{
    public string MemberId { get; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime MemberSince { get; set; }    

    private List<Book> borrowedBooks = new List<Book>();

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
        return $"Member ID: {MemberId}, Name: {Name}, Email: {Email}, Member Since: {MemberSince.ToShortDateString()}, Borrowed Books: {borrowedBooks.Count}";
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