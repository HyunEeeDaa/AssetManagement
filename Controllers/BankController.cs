using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AssetManagement.Controllers
{
	public class BankController : BaseController
	{
		private asset_managementContext _dbContext;

		public BankController(asset_managementContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult BankAccount()
		{
			return View();
		}

		[HttpPost]
		public void NewBankAccount(string card_number, string card_holder, string bank_inistitution, string bank_type, string expiry_date)
		{
			Account? account = GetCurrentUser();
			if (account == null)
			{
				return;
			}
			BankAccount bankAccount = new BankAccount();
			bankAccount.CardNumber = card_number;
			bankAccount.CardHolder = card_holder;
			bankAccount.BankInistitution = bank_inistitution;
			bankAccount.Type = bank_type;
			bankAccount.ExpiryDate = DateTime.Parse(expiry_date);
			bankAccount.AccountId = account.Id;

				_dbContext.BankAccounts.Add(bankAccount);
				_dbContext.SaveChanges();
		}

		public IActionResult AccountList()
		{
			Account? account = GetCurrentUser();
			if(account == null)
			{
				RedirectToAction("Index", "Home");
			}
			List<BankAccount> accounts = _dbContext.BankAccounts.Where(o => o.AccountId == account!.Id).ToList();
			return View(accounts);
		}
	}
}
