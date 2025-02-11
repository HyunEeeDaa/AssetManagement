using AssetManagement.Models;

namespace AssetManagement.DTOs
{
    public class TransactionViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
    
}
