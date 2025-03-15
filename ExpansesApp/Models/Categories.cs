using ExpansesApp.Models;

namespace ExpensesApp.Models
{
    public class Categories
    {

        public int Id { get; set; }
        public string Expanses { get; set; }
        public ICollection<Expenses> Expenses { get; set; }
    }

}