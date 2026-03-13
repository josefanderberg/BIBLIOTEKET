using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIBLIOTEK.Core;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllMembersAsync();
    Task<Member?> GetByIdAsync(int id);
    Task<Member?> GetByMemberIdAsync(string memberId);
    Task AddMemberAsync(Member member);
    Task UpdateMemberAsync(Member member);
    Task DeleteMemberAsync(int id);
    Task<IEnumerable<Member>> SearchAsync(string searchTerm);
}
