using System.Collections.Generic;

namespace ExpensesApp.Models
{
    public class ExpensesViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
        public int? SelectedCategoryId { get; set; }
    }
}