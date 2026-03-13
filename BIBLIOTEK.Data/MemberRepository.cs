using Microsoft.EntityFrameworkCore;
using BIBLIOTEK.Core;

namespace BIBLIOTEK.Data;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryContext _context;

    public MemberRepository(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync()
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ToListAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        return await _context.Members
            .Include(m => m.Loans)
            .ThenInclude(l => l.Book)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Member?> GetByMemberIdAsync(string memberId)
    {
        return await _context.Members
            .Include(m => m.Loans)
            .FirstOrDefaultAsync(m => m.MemberId == memberId);
    }

    public async Task AddMemberAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateMemberAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMemberAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member != null)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Member>> SearchAsync(string searchTerm)
    {
        return await _context.Members
            .Include(m => m.Loans)
            .Where(m => m.Name.ToLower().Contains(searchTerm.ToLower()) ||
                        m.Email.ToLower().Contains(searchTerm.ToLower()) ||
                        m.MemberId.ToLower().Contains(searchTerm.ToLower()))
            .ToListAsync();
    }
}
