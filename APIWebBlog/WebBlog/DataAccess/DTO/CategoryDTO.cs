using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class CategoryDTO
    {
        public int category_id { get; set; }
        public string? categoryName { get; set; }
        public int published { get; set; }

    }
}
