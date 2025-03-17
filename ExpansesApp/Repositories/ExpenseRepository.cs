using ExpensesApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using YourProjectNamespace.Repositories;

namespace ExpensesApp.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpensesDBContext _context;

        public ExpenseRepository(ExpensesDBContext context)
        {
            _context = context;
        }

        public decimal GetTotalExpenses()
        {
            return _context.Expenses.Sum(e => e.Price);
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            return _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.Category != null) // Filtruj wydatki z przypisaną kategorią
                .GroupBy(e => e.Category!.Name) // Użyj operatora !, aby potwierdzić, że Category nie jest null
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Price));
        }

        public decimal GetAverageExpensesPerDay()
        {
            var totalExpenses = GetTotalExpenses();
            var days = (DateTime.Now - _context.Expenses.Min(e => e.DateAdded)).Days;
            return days > 0 ? totalExpenses / days : 0;
        }
    }
}