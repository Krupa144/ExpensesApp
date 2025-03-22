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
           .HasOne(e => e.User) 
           .WithMany() 
           .HasForeignKey(e => e.UserId) 
           .OnDelete(DeleteBehavior.Cascade);
                base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}