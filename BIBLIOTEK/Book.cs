public class Book : ISearchable
{
    public string ISBN { get; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; }    

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
        return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Published: {PublishedYear}, Available: {IsAvailable}";
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