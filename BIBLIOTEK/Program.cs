class Program
{
    static void Main(string[] args)
    {
        var library = new Library();
        InitializeLibrary(library);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Bibliotekssystem ===");
            Console.WriteLine("1. Visa alla böcker");
            Console.WriteLine("2. Sök bok");
            Console.WriteLine("3. Låna bok");
            Console.WriteLine("4. Returnera bok");
            Console.WriteLine("5. Visa medlemmar");
            Console.WriteLine("6. Statistik");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    library.ListAllBooks();
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Sökterm: ");
                    var term = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(term))
                    {
                        library.SearchBooks(term);
                    }
                    else
                    {
                        Console.WriteLine("Du måste ange en sökterm.");
                    }
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Ange ISBN: ");
                    var isbn = Console.ReadLine();
                    Console.Write("Ange medlems-ID: ");
                    var memberId = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(isbn) && !string.IsNullOrWhiteSpace(memberId))
                    {
                        library.BorrowBook(isbn, memberId);
                    }
                    else
                    {
                        Console.WriteLine("Felaktig inmatning. Både ISBN och Medlems-ID krävs.");
                    }
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Console.Write("Ange ISBN: ");
                    var rIsbn = Console.ReadLine();
                    Console.Write("Ange medlems-ID: ");
                    var rMemberId = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(rIsbn) && !string.IsNullOrWhiteSpace(rMemberId))
                    {
                        library.ReturnBook(rIsbn, rMemberId);
                    }
                    else
                    {
                        Console.WriteLine("Felaktig inmatning. Både ISBN och Medlems-ID krävs.");
                    }
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "5":
                    Console.Clear();
                    library.ListAllMembers();
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "6":
                    Console.Clear();
                    library.ShowStatistics();
                    Console.WriteLine("\nTryck på valfri tangent för att återgå...");
                    Console.ReadKey();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void InitializeLibrary(Library lib)
    {
        // Add some books
        lib.AddBook("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954);
        lib.AddBook("978-1-2-345678-9", "Hobbiten", "J.R.R. Tolkien", 1937);
        lib.AddBook("978-0-13-235088-4", "Clean Code", "Robert C. Martin", 2008);

        // Add some members
        lib.AddMember("M001", "Anna Andersson", "anna@example.com");
        lib.AddMember("M002", "Björn Borg", "bjorn@example.com");
    }
}