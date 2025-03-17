namespace ExpensesApp.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty; // Zainicjalizuj domyślną wartością
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>(); // Zainicjalizuj domyślną wartością
    }
}