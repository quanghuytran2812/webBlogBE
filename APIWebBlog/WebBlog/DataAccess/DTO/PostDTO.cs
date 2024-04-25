using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class PostDTO
    {
        public int post_id { get; set; }
        public string? thumb { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? summary { get; set; }
        public int published { get; set; }
        public int UserId { get; set; }
        public int? TabId { get; set; }
        public int? CategoryId { get; set; }

    }
}
