﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHaven.Domain.Entities
{
    public class Book
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required (ErrorMessage ="Enter a valid Title")]
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }

        [Required, MinLength(4, ErrorMessage = "minimum lenght is 20")]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        //public long CategoryId { get; set; }
        public ICollection<Category> Category { get; set; } = new List<Category>();
    }
}
