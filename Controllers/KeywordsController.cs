using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssetManagement.Models;

namespace AssetManagement.Controllers
{
    public class KeywordsController : BaseController
    {
        private asset_managementContext _dbContext;

        public KeywordsController(asset_managementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Keywords
        public async Task<IActionResult> Index()
        {
            var asset_managementContext = _dbContext.Keywords.Include(k => k.Account).Include(k => k.Category);
            return View(await asset_managementContext.ToListAsync());
        }

        // GET: Keywords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _dbContext.Keywords
                .Include(k => k.Account)
                .Include(k => k.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // GET: Keywords/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "CategoryName");
            Account? account = GetCurrentUser();
            if (account == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Category> categories = _dbContext.Categories.Where(o => o.AccountId == account.Id).ToList();
            List<SelectListItem> cateogryOptions = new List<SelectListItem>();
            foreach(Category category in categories)
            {
                SelectListItem selectListItem = new SelectListItem(category.CategoryName, category.Id.ToString());
                cateogryOptions.Add(selectListItem);
            }
            return View(cateogryOptions);
        }

        // POST: Keywords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int CategoryId, string KeywordName)
        {
            try
            {
                Account? account = GetCurrentUser();
                if (account == null)
                {
                    return RedirectToAction("Index", "Home");
                }
               // Category category = _dbContext.Categories.Where(o => o.CategoryName == CategoryName).First();

                Keyword keyword = new Keyword();
                keyword.KeywordName = KeywordName;
                keyword.CategoryId = CategoryId;
                keyword.AccountId = account!.Id;
                _dbContext.Keywords.Add(keyword);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
           
        }

        // GET: Keywords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _dbContext.Keywords.FindAsync(id);
            if (keyword == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_dbContext.Accounts, "Id", "Email", keyword.AccountId);
            ViewData["CategoryId"] = new SelectList(_dbContext.Categories, "Id", "CategoryName", keyword.CategoryId);
            return View(keyword);
        }

        // POST: Keywords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, int CategoryId, string KeywordName)
        {
            try
            {
                Keyword? keyword = _dbContext.Keywords.Where(o => o.Id == Id).FirstOrDefault();
                if (keyword == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                keyword.KeywordName = KeywordName;
                keyword.CategoryId = CategoryId;
                keyword.Id = Id;
                _dbContext.Keywords.Update(keyword);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Keywords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keyword = await _dbContext.Keywords
                .Include(k => k.Account)
                .Include(k => k.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (keyword == null)
            {
                return NotFound();
            }

            return View(keyword);
        }

        // POST: Keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keyword = await _dbContext.Keywords.FindAsync(id);
            if (keyword != null)
            {
                _dbContext.Keywords.Remove(keyword);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordExists(int id)
        {
            return _dbContext.Keywords.Any(e => e.Id == id);
        }
    }
}
