public class Member
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

    public void GetInfo()
    {
        Console.WriteLine($"Member ID: {MemberId}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Email: {Email}");
        Console.WriteLine($"Member Since: {MemberSince}");
        Console.WriteLine($"Borrowed Books: {borrowedBooks.Count}");
    }       
}