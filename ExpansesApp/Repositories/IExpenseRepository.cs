using ExpensesApp.Models;
using System.Collections.Generic;

namespace ExpensesApp.Repositories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> GetAllExpenses();
        Expense GetExpenseById(int id);
        void AddExpense(Expense expense);
        void UpdateExpense(Expense expense);
        void DeleteExpense(int id);

        // Dodaj brakujące metody
        Dictionary<string, decimal> GetExpensesByCategory();
        decimal GetAverageExpensesPerDay();
    }
}