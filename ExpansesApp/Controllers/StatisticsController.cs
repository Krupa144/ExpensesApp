using Microsoft.AspNetCore.Mvc;
using ExpensesApp.Repositories;
using System;
using System.Collections.Generic;
using ExpensesApp.Models;
using YourProjectNamespace.Repositories;

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
            try
            {
                var totalExpenses = _expenseRepository.GetTotalExpenses();
                var expensesByCategory = _expenseRepository.GetExpensesByCategory();
                var averageExpensesPerDay = _expenseRepository.GetAverageExpensesPerDay();

                var model = new StatisticsViewModel
                {
                    TotalExpenses = totalExpenses,
                    ExpensesByCategory = expensesByCategory,
                    AverageExpensesPerDay = averageExpensesPerDay
                };

                return View(model); // Zwróć widok z danymi
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Wystąpił błąd podczas pobierania statystyk: " + ex.Message;
                return View("Error"); // Możesz utworzyć widok "Error" do wyświetlania błędów
            }
        }
    }
}
