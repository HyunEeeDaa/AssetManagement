using AssetManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace AssetManagement.Controllers
{
	public class CategoryController : BaseController
	{
		private asset_managementContext _dbContext;

		public CategoryController(asset_managementContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		// GET: CategoryController
		public ActionResult Index()
		{
			Account? account = GetCurrentUser();
			if (account == null)
			{
				RedirectToAction("Index", "Home");
			}
			List<Category> categories = _dbContext.Categories.Where(o => o.AccountId == account!.Id).ToList();
			return View(categories);

		}

        

        // GET: CategoryController/Details/5
        public ActionResult Details(int Id)
		{
			var categoryDetails = _dbContext.Categories.Where(o => o.Id == Id).FirstOrDefault();
            return View(categoryDetails);
		}

		// GET: CategoryController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(string CategoryName)
		{
			try
			{
				Account? account = GetCurrentUser();
				if (account == null)
				{
					return RedirectToAction("Index", "Home");
				}
				Category category = new Category();
				category.CategoryName = CategoryName;
				category.AccountId = account!.Id;
				_dbContext.Categories.Add(category);
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception error)
			{
				string errorMsg = error.Message;
				return View();
			}
		}

		// GET: CategoryController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CategoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int Id, string CategoryName)
		{
			try
			{
                Category? category = _dbContext.Categories.Where(o => o.Id == Id).FirstOrDefault();
				if(category == null)
				{
                    return RedirectToAction("Index", "Home");
                }
				category.CategoryName = CategoryName;
				category.Id = Id;
				_dbContext.Categories.Update(category);
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	

		// GET: CategoryController/Delete/5
		public ActionResult Delete(int? Id)
		{
			var categoryName = _dbContext.Categories.Where(o => o.Id == Id).FirstOrDefault();
            return View(categoryName);
        }

		// POST: CategoryController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int Id)
		{
			try
			{
                Category? category = _dbContext.Categories.Where(o => o.Id == Id).FirstOrDefault();
                if (category == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
			catch
			{
				return View();
			}
		}
	}
}
