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

        public DateTime DateAdded { get; set; } = DateTime.UtcNow; // Ustaw domyślną wartość na bieżącą datę

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        // Relacja z użytkownikiem
        public string UserId { get; set; } // Identyfikator użytkownika
        public ApplicationUser User { get; set; } // Nawigacyjna właściwość do użytkownika
    }
}