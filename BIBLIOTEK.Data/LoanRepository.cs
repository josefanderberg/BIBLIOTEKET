using Microsoft.EntityFrameworkCore;
using BIBLIOTEK.Core;

namespace BIBLIOTEK.Data;

public class LoanRepository : ILoanRepository
{
    private readonly LibraryContext _context;

    public LoanRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Loan>> GetAllLoansAsync()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .Where(l => l.ReturnDate == null)
            .ToListAsync();
    }

    public async Task<Loan?> GetByIdAsync(int id)
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task AddLoanAsync(Loan loan)
    {
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLoanAsync(Loan loan)
    {
        _context.Loans.Update(loan);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLoanAsync(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan != null)
        {
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId)
    {
        return await _context.Loans
            .Include(l => l.Member)
            .Where(l => l.BookId == bookId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Loan>> GetLoansByMemberIdAsync(int memberId)
    {
        return await _context.Loans
            .Include(l => l.Book)
            .Where(l => l.MemberId == memberId)
            .ToListAsync();
    }
}
