using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string? categoryName { get; set; }
        public int published { get; set; }
        public virtual ICollection<Posts>? Posts { get; set; }
    }
}
