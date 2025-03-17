using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpensesApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using YourProjectNamespace.Repositories;

namespace ExpensesApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesDBContext _context;

        public ExpensesController(ExpensesDBContext context)
        {
            _context = context;
        }

        // GET: Expenses/Index
        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _context.Categories.ToListAsync();
            var expensesQuery = _context.Expenses.Include(e => e.Category).AsQueryable();

            if (categoryId.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.CategoryId == categoryId);
            }

            var model = new ExpensesViewModel
            {
                Expenses = await expensesQuery.ToListAsync(),
                Categories = categories,
                SelectedCategoryId = categoryId
            };

            return View(model);
        }

        // GET: Expenses/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name");
            return View();
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                if (expense.CategoryId == 0)
                {
                    ModelState.AddModelError("CategoryId", "Wybierz kategorię.");
                    ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name");
                    return View(expense);
                }

                // Przypisz istniejącą kategorię do wydatku
                var category = await _context.Categories.FindAsync(expense.CategoryId);
                if (category == null)
                {
                    ModelState.AddModelError("CategoryId", "Wybrana kategoria nie istnieje.");
                    ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name");
                    return View(expense);
                }

                expense.Category = category;

                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name");
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name", expense.CategoryId);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = await _context.Categories.FindAsync(expense.CategoryId);
                if (category == null)
                {
                    ModelState.AddModelError("CategoryId", "Wybrana kategoria nie istnieje.");
                    ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name", expense.CategoryId);
                    return View(expense);
                }

                expense.Category = category;

                _context.Update(expense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "ID", "Name", expense.CategoryId);
            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound(); // Jeśli nie znaleziono wydatku, zwróć błąd 404
            }

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Po usunięciu przekieruj do listy
        }




        // GET: Expenses/ManageCategories
        public async Task<IActionResult> ManageCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // GET: Expenses/CreateCategory
        public IActionResult CreateCategory()
        {
            return View();
        }

        // POST: Expenses/CreateCategory
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Expenses/DeleteCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(); 
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageCategories)); 
        }
        [ApiController]
        [Route("api/[controller]")]
        public class StatisticsController : ControllerBase
        {
            private readonly IExpenseRepository _expenseRepository;

            public StatisticsController(IExpenseRepository expenseRepository)
            {
                _expenseRepository = expenseRepository;
            }

            [HttpGet("total")]
            public IActionResult GetTotalExpenses()
            {
                var total = _expenseRepository.GetTotalExpenses();
                return Ok(total);
            }

            [HttpGet("byCategory")]
            public IActionResult GetExpensesByCategory()
            {
                var expensesByCategory = _expenseRepository.GetExpensesByCategory();
                return Ok(expensesByCategory);
            }

            [HttpGet("averagePerDay")]
            public IActionResult GetAverageExpensesPerDay()
            {
                var average = _expenseRepository.GetAverageExpensesPerDay();
                return Ok(average);
            }
        }


    }
}
