using Microsoft.EntityFrameworkCore;
using BIBLIOTEK.Core;

namespace BIBLIOTEK.Data;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Loan> Loans { get; set; }

    // Parameterlös konstruktor för normal användning
    public LibraryContext() { }

    // Options-konstruktor för testning (InMemory-databas)
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }
}