using AssetManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AssetManagement.Controllers
{
    public class AccountController : BaseController
    {
        private asset_managementContext _dbContext;

        public AccountController(asset_managementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Register()
        {
            return View();
        }
        //login index 추가, view 추가.
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Account? account = _dbContext.Accounts.Where(o => o.Username == username && o.Password == password).FirstOrDefault();

            if(account == null)
            {
                return View();
            }
            SetAccountSession(account);   
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void RegisterNewAccount(string username, string email, string name, string password)
        {           
            Account account = new Account();
            account.Username = username;
            account.Name = name;
            account.Password = password;
            account.Email = email;
                
            _dbContext.Accounts.Add(account); // database 넣는 준비한는 코드
            _dbContext.SaveChanges();// database에 실질적인 저장
        }
    }
    
}
