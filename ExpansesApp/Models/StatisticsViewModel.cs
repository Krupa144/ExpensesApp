namespace ExpensesApp.Models
{
    public class StatisticsViewModel
    {
        public decimal TotalExpenses { get; set; }
        public decimal AverageExpensesPerDay { get; set; }
        public Dictionary<string, decimal> ExpensesByCategory { get; set; }
    }
}