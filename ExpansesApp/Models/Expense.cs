using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesApp.Models
{
    public class Expense
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string UserId { get; set; } 
        public ApplicationUser User { get; set; } 
    }
}