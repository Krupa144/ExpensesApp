using ExpensesApp.Models; // Poprawiona przestrzeń nazw
using Microsoft.EntityFrameworkCore;

namespace ExpensesApp.Models // Zmieniona na poprawną przestrzeń nazw
{
    public class ExpensesDBContext : DbContext // Zmieniona nazwa klasy, aby pasowała do nazwy przestrzeni
    {
        public ExpensesDBContext(DbContextOptions<ExpensesDBContext> options)
            : base(options)
        {
        }

        public DbSet<Expenses> Expenses { get; set; } // DbSet dla encji Expense (pierwsza litera w nazwie klasy powinna być wielka)
    }
}
