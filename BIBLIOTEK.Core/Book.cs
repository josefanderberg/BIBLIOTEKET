using System.ComponentModel.DataAnnotations;

namespace BIBLIOTEK.Core;

public class Book : ISearchable
{
    public int Id { get; set; }
    [Required(ErrorMessage = "ISBN krävs")]
    [RegularExpression(@"^(\d{10}|\d{13})$", ErrorMessage = "ISBN måste vara 10 eller 13 siffror")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Titel krävs")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Författare krävs")]
    public string Author { get; set; } = string.Empty;

    [Range(1000, 2100, ErrorMessage = "Ogiltigt år")]
    public int PublishedYear { get; set; } = DateTime.Now.Year;

    public bool IsAvailable { get; set; } = true;    
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