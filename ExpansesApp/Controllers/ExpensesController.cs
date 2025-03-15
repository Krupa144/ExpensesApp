using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpansesApp.Models; // Dodaj przestrzeń nazw dla modelu Expenses
using System.Linq;
using System.Threading.Tasks;
using ExpensesApp.Models;

namespace ExpansesApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ExpensesDBContext _context;

        // Konstruktor z wstrzyknięciem zależności kontekstu bazy danych
        public ExpensesController(ExpensesDBContext context)
        {
            _context = context;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            if (!_context.Expenses.Any()) // Sprawdzamy, czy tabela jest pusta
            {
                // Dodaj przykładowe dane, jeśli tabela jest pusta
                _context.Expenses.Add(new Expenses { Name = "Sample Expense", Description = "Example description", Price = 100.00m });
                await _context.SaveChangesAsync();
            }

            var expenses = await _context.Expenses.ToListAsync(); // Pobieramy wszystkie dane
            return View(expenses);
        }


        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View(); // Zwracamy widok do tworzenia nowego elementu
        }

        // POST: Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expenses expenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Przekierowanie na stronę główną po dodaniu
            }
            return View(expenses); // Jeśli model jest niepoprawny, wróć do widoku
        }


        // GET: Expenses/Edit/5
        // Metoda do edytowania
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }

            return View(expenses);
        }

        // Metoda do zapisywania zmian
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expenses expenses)
        {
            if (id != expenses.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Expenses.Any(e => e.ID == expenses.ID))
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
            return View(expenses);
        }


        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var expenses = await _context.Expenses.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Przekierowanie na stronę główną po usunięciu
        }


        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense); // Usuwamy element z kontekstu
                await _context.SaveChangesAsync(); // Zapisujemy zmiany w bazie danych
            }
            return RedirectToAction(nameof(Index)); // Przekierowanie do widoku Index po usunięciu
        }
    }
}
