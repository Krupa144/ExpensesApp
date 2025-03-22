using ExpensesApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ExpensesApp.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpensesDBContext _context;

        public ExpenseRepository(ExpensesDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return _context.Expenses.ToList();
        }

        public Expense GetExpenseById(int id)
        {
            return _context.Expenses.Find(id);
        }

        public void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public void UpdateExpense(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChanges();
        }

        public void DeleteExpense(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
        }

        public Dictionary<string, decimal> GetExpensesByCategory()
        {
            return _context.Expenses
                .GroupBy(e => e.Category.Name)
                .ToDictionary(g => g.Key, g => g.Sum(e => e.Price));
        }

        public decimal GetAverageExpensesPerDay()
        {
            var totalExpenses = _context.Expenses.Sum(e => e.Price);
            var totalDays = _context.Expenses
                .Select(e => e.DateAdded.Date)
                .Distinct()
                .Count();

            return totalDays == 0 ? 0 : totalExpenses / totalDays;
        }
    }
}