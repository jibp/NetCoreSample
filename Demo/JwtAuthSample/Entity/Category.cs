using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Entity
{
    public class Category
    {
        [Key]
        [MaxLength(50)]
        public string Id { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        public List<Product> ProductList { get; set; }
    }
}
