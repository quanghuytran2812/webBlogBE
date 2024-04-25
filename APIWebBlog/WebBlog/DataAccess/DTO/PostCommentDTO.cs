using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class PostCommentDTO
    {
        public int postCo_id { get; set; }
        public string? content { get; set; }
        public int published { get; set; }  
        public DateTime created_time { get; set; }
       public int PostId { get; set; }
       public int UserId { get; set; }
    }
}
