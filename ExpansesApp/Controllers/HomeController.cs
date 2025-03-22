using ExpensesApp.Models;
using ExpensesApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public HomeController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult Index()
        {
            var expensesByCategory = _expenseRepository.GetExpensesByCategory;
            var averageExpensesPerDay = _expenseRepository.GetAverageExpensesPerDay;

            ViewBag.ExpensesByCategory = expensesByCategory;
            ViewBag.AverageExpensesPerDay = averageExpensesPerDay;

            return View();
        }
    }
}