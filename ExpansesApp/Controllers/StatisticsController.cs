using ExpensesApp.Models;
using ExpensesApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;

        public StatisticsController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public IActionResult Index()
        {
            var totalExpenses = _expenseRepository.GetAllExpenses().Sum(e => e.Price);
            var expensesByCategory = _expenseRepository.GetExpensesByCategory();
            var averageExpensesPerDay = _expenseRepository.GetAverageExpensesPerDay();

            var viewModel = new StatisticsViewModel
            {
                TotalExpenses = totalExpenses,
                ExpensesByCategory = expensesByCategory,
                AverageExpensesPerDay = averageExpensesPerDay
            };

            return View(viewModel);
        }
    }
}