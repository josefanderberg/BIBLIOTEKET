using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using BIBLIOTEK.Core;
using BIBLIOTEK.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Registrera Entity Framework och Repository
builder.Services.AddDbContext<LibraryContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();

var app = builder.Build();

// Se till att databasen skapas och har seed-data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    context.Database.EnsureCreated();

    // Lägg till exempeldata om databasen är tom
    if (!context.Books.Any())
    {
        context.Books.AddRange(
            new Book("978-91-0-012345-6", "Sagan om ringen", "J.R.R. Tolkien", 1954),
            new Book("978-1-2-345678-9", "Hobbiten", "J.R.R. Tolkien", 1937),
            new Book("978-0-13-235088-4", "Clean Code", "Robert C. Martin", 2008),
            new Book("978-0-201-63361-0", "Design Patterns", "Gang of Four", 1994),
            new Book("978-91-0-056789-0", "1984", "George Orwell", 1949)
        );

        context.Members.AddRange(
            new Member("M001", "Anna Andersson", "anna@example.com"),
            new Member("M002", "Björn Borg", "bjorn@example.com"),
            new Member("M003", "Clara Carlsson", "clara@example.com")
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
