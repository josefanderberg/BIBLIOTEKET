public class Library
{
    private BookCatalog bookCatalog = new BookCatalog();
    private MemberRegistry memberRegistry = new MemberRegistry();
    private LoanManager loanManager = new LoanManager();

    // --- Book Management ---
    public void AddBook(string isbn, string title, string author, int year)
    {
        var book = new Book(isbn, title, author, year);
        bookCatalog.AddBook(book);
    }

    public void ListAllBooks()
    {
        var books = bookCatalog.GetAllBooks();
        foreach (var book in books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }
    
    public void SearchBooks(string searchTerm)
    {
        var results = bookCatalog.SearchBooks(searchTerm);
        if (results.Count == 0)
        {
            Console.WriteLine("No books found matching the search term.");
        }
        else
        {
            foreach (var book in results)
            {
                Console.WriteLine(book.GetInfo());
            }
        }
    }

    // --- Member Management ---
    public void AddMember(string id, string name, string email)
    {
        var member = new Member(id, name, email);
        memberRegistry.AddMember(member);
    }

    public void ListAllMembers()
    {
        var members = memberRegistry.GetAllMembers();
        foreach (var member in members)
        {
            Console.WriteLine(member.GetInfo());
        }
    }

    // --- Loan Management ---
    public void BorrowBook(string isbn, string memberId)
    {
        var book = bookCatalog.GetBookByISBN(isbn);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        var member = memberRegistry.GetMemberById(memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        try
        {
            loanManager.CreateLoan(book, member);
            Console.WriteLine($"Successfully borrowed '{book.Title}' to {member.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ReturnBook(string isbn, string memberId)
    {
        var book = bookCatalog.GetBookByISBN(isbn);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        var member = memberRegistry.GetMemberById(memberId);
        if (member == null)
        {
            Console.WriteLine("Member not found.");
            return;
        }

        try
        {
            loanManager.ReturnLoan(book, member);
            Console.WriteLine($"Successfully returned '{book.Title}' from {member.Name}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // --- Statistics ---
    public void ShowStatistics()
    {
        Console.WriteLine($"Total Books: {bookCatalog.GetTotalBookCount()}");
        Console.WriteLine($"Active Loans: {loanManager.GetTotalLoanCount()}"); // Using total count for now, maybe want active count later
    }
}
