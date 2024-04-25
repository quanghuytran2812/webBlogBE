using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class TabDTO
    {
        public int tab_id { get; set; }

        public string? tabName { get; set; }
        public int? published { get; set; }
    }
}
