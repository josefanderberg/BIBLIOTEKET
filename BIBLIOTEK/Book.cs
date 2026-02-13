public class Book
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

    public void GetInfo()           
    {
        Console.WriteLine($"ISBN: {ISBN}");
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Published Year: {PublishedYear}");
        Console.WriteLine($"Is Available: {IsAvailable}");
    }
}   