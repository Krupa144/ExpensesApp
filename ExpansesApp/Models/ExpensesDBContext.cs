using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpensesApp.Models;

namespace ExpensesApp.Models
{
    public class ExpensesDBContext : IdentityDbContext<ApplicationUser>
    {
        public ExpensesDBContext(DbContextOptions<ExpensesDBContext> options) : base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Expense>()
           .HasOne(e => e.User) // Expense ma jednego użytkownika
           .WithMany() // Użytkownik może mieć wiele wydatków
           .HasForeignKey(e => e.UserId) // Klucz obcy to UserId
           .OnDelete(DeleteBehavior.Cascade);
                base.OnModelCreating(modelBuilder); // Ważne dla Identity

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}