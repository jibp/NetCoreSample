using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Entity
{
    public class Product
    {
        [Key]
        [Required]
        [MaxLength(50)]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public double Price { get; set; }

        [MaxLength(50)]
        public string CategoryId { get; set; }

        public Category CategoryEntity { get; set; }
        public Book Book { get; set; }
    }
}
