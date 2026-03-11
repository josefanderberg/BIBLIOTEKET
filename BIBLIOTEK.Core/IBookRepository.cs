namespace BIBLIOTEK.Core;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetByIdAsync(int id);          // Behövs för bokdetaljer-sidan
    Task<Book?> GetBookByIsbnAsync(string isbn);
    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);              // Enklare med int id istället för Book
    Task<IEnumerable<Book>> SearchAsync(string searchTerm);  // Behövs för sökfunktionen
}
