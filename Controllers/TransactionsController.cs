using AssetManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.Globalization;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;

namespace AssetManagement.Controllers
{
    public class TransactionsController : BaseController
    {
        private readonly asset_managementContext _dbContext;

        public TransactionsController(asset_managementContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Transactions.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile csvFile)
        {

            List<Transaction> transactions = new List<Transaction>();   
            

            using (var stream = new StreamReader(csvFile.OpenReadStream(), Encoding.UTF8))
            {
                // Skip the header row
                stream.ReadLine();
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    if (line != null)
                    {
                       
                        var values = line.Split(',');
                        DateTime date = DateTime.Parse(values[0]);
                        string merchant = values[1];
                        double outgoingAmount = values[2] != "" ? double.Parse(values[2]) : 0;
                        double incomingAmount = values[3] != "" ? double.Parse(values[3]) : 0;
                        int categoryId = await CheckKeyword(merchant);
                        Transaction transaction = new Transaction();
                        transaction.Merchant = merchant;
                        transaction.Date = date;
                        transaction.IncomingAmount = incomingAmount;
                        transaction.OutgoingAmount = outgoingAmount;
                        transaction.CategoryId = categoryId;
                        _dbContext.Transactions.Add(transaction);
                        _dbContext.SaveChanges();
                        

                    }
                }
            }
            var a = transactions;
            return RedirectToAction(nameof(Index));
        }

        public async Task<int> CheckKeyword(string merchant)
        {
            // call from database
            List<Keyword> keywords = await _dbContext.Keywords.ToListAsync();

            foreach(var keyword in keywords)
            {
                if(merchant.ToLower().Contains(keyword.KeywordName.ToLower()))
                {
                    return keyword.CategoryId;
                }
            }
            return 0;


        }
        // GET: Transactions/Details/5\\
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Merchant,OutgoingAmount,IncomingAmount")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.CategoryId = await CheckKeyword(transaction.Merchant);
                _dbContext.Add(transaction);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Merchant,OutgoingAmount,IncomingAmount")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    transaction.CategoryId = await CheckKeyword(transaction.Merchant);
                    _dbContext.Update(transaction);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _dbContext.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _dbContext.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _dbContext.Transactions.Any(e => e.Id == id);
        }
    }
}
