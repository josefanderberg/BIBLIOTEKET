namespace BIBLIOTEK.Core;

public class Book : ISearchable
{
    public int Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; }    
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();


    // Parameterlös konstruktor krävs av Entity Framework
    public Book() { }

    public Book(string isbn, string title, string author, int publishedYear)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        PublishedYear = publishedYear;
        IsAvailable = true;
    }

    public string GetInfo()
    {
        string status = IsAvailable ? "Tillgänglig" : "Utlånad";
        return $"\"{Title}\" av {Author} ({PublishedYear}) - {status}\n\tISBN: {ISBN}";
    }

    public bool Matches(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return false;
        searchTerm = searchTerm.ToLower();
        return Title.ToLower().Contains(searchTerm) || 
               Author.ToLower().Contains(searchTerm) || 
               ISBN.Contains(searchTerm);
    }
}   