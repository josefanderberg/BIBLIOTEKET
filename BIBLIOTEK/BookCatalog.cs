public class BookCatalog
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public Book GetBookByISBN(string isbn)
    {
        return books.FirstOrDefault(b => b.ISBN == isbn);
    }

    public List<Book> GetAllBooks()
    {
        return new List<Book>(books);
    }

    public List<Book> SearchBooks(string searchTerm)
    {
        return books.Where(b => b.Matches(searchTerm)).ToList();
    }

    public int GetTotalBookCount()
    {
        return books.Count;
    }


}
