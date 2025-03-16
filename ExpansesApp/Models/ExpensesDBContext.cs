using Microsoft.EntityFrameworkCore;

namespace ExpensesApp.Models
{
    public class ExpensesDBContext : DbContext
    {
        public ExpensesDBContext(DbContextOptions<ExpensesDBContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}