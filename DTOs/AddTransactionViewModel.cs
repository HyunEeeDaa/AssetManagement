using AssetManagement.Models;

namespace AssetManagement.DTOs
{
    public class AddTransactionViewModel
    {
        public DateTime Date { get; set; }

        public string Merchant { get; set; }

        public double OutgoingAmount { get; set; }

        public double IncomingAmount { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
    }
}
