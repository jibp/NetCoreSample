using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthSample.Entity
{
    public class Book
    {
        public string Id { get; set; }

        [Display(Description = "版号")]
        public string ISBN { get; set; }

        public Product product { get; set; }
    }
}
