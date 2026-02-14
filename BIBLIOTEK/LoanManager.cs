public class LoanManager
{
    private List<Loan> loans = new List<Loan>();

    public void AddLoan(Loan loan)
    {
        loans.Add(loan);
    }

    public Loan GetLoanByLoanId(string loanId)
    {
        return loans.FirstOrDefault(l => l.LoanId == loanId);
    }

    public List<Loan> GetAllLoans()
    {
        return new List<Loan>(loans);
    }

    public int GetTotalLoanCount()
    {
        return loans.Count;
    }   

}
