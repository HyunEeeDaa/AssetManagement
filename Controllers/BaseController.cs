using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagement.Controllers
{
	public class BaseController : Controller
	{
		private asset_managementContext _dbContext;
		public BaseController(asset_managementContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Account? GetCurrentUser()
		{
			int? id = HttpContext.Session.GetInt32("CurrentUseId");
			if (id == null)
			{
				return null;
			}
			Account? account = _dbContext.Accounts.Where(o => o.Id == id).FirstOrDefault();
			return account;
		}
        public void SetAccountSession(Account account)
		{
			HttpContext.Session.SetString("CurrentUserEmail", account.Email);
			HttpContext.Session.SetInt32("CurrentUseId", account.Id);
		}
		public void resetAccoutSession()
		{
			HttpContext.Session.Clear();
        }
	}
}
