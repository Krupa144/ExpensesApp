using System;
using System.Collections.Generic;

namespace YourProjectNamespace.Repositories // Zmień na odpowiednią przestrzeń nazw
{
    public interface IExpenseRepository
    {
        decimal GetTotalExpenses(); // Suma wszystkich wydatków
        Dictionary<string, decimal> GetExpensesByCategory(); // Wydatki pogrupowane według kategorii
        decimal GetAverageExpensesPerDay(); // Średnie wydatki dzienne
    }
}