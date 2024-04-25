using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class PostLikeDTO
    {
        public int postL_id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime created_time { get; set; }
    }
}
