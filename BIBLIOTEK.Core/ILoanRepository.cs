using System.Collections.Generic;
using System.Threading.Tasks;

namespace BIBLIOTEK.Core;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllLoansAsync();
    Task<IEnumerable<Loan>> GetActiveLoansAsync();
    Task<Loan?> GetByIdAsync(int id);
    Task AddLoanAsync(Loan loan);
    Task UpdateLoanAsync(Loan loan);
    Task DeleteLoanAsync(int id);
    Task<IEnumerable<Loan>> GetLoansByBookIdAsync(int bookId);
    Task<IEnumerable<Loan>> GetLoansByMemberIdAsync(int memberId);
}
