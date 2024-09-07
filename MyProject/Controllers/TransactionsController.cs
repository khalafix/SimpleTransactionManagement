using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyProject.DataModel;
using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationContext _context;
        private     readonly IWebHostEnvironment _webHostEnvironment;
        public TransactionsController(ApplicationContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Transactions.Include(t => t.BankAccount).Include(t => t.Customer).Include(t => t.User);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.BankAccount)
                .Include(t => t.Customer)
                .Include(t => t.User)
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
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "AccountNumber");
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionViewModel model )
        {
            if (ModelState.IsValid)
            {
                if (model.DepositReceiptFile != null)
                {
                    string filename = $"{Guid.NewGuid().ToString()}_{model.DepositReceiptFile.FileName}";
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads",filename);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.DepositReceiptFile.CopyToAsync(stream);
                    }
                    model.DepositReceipt = filename;
                }
                User customer = new User()
                {
                    UserName = $"{model.FirstName}_{model.LastName}",
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                _context.Users.Add(customer);
                Transaction transaction = new Transaction() { 
                     BankAccountId = model.BankAccountId,
                     CustomerId = customer.Id,
                     DepositReceipt = model.DepositReceipt,
                     DepositDateTime = model.DepositDateTime,
                     DepositAmount = model.DepositAmount,

                };
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "AccountNumber", model.BankAccountId);
           // ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName", model.CustomerId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", model.UserId);
            return View(model);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "AccountNumber", transaction.BankAccountId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName", transaction.CustomerId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", transaction.UserId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BankAccountId,UserId,CustomerId,DepositDateTime,DepositAmount,DepositReceipt")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
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
            ViewData["BankAccountId"] = new SelectList(_context.BankAccounts, "Id", "AccountNumber", transaction.BankAccountId);
            //ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "UserName", transaction.CustomerId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", transaction.UserId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.BankAccount)
                .Include(t => t.Customer)
                .Include(t => t.User)
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
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
        public IActionResult Inquery()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Inquery(TransactionInqueryViewModel model)
        {

            if (ModelState.IsValid)
            {
                // جستجوی شماره حساب متناسب با مبلغ واریزی
                try
                {
                    var bankAccount = _context.Database.SqlQuery<string>($"exec GetBankAccountByDepositAmount {model.DepositAmount}").ToList();
                    if (bankAccount != null)
                    {
                        model.DateTime = DateTime.Now;
                        model.AccountNumber = bankAccount.FirstOrDefault()!.ToString();
                    }
                }
                catch (Exception ex)
                {

                    ViewData["errorMessage"] = ex.Message ;
                }

            }
            return View(model);
        }
    }
}
