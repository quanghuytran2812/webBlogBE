using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Post_Like
    {
        [Key]
        public int postL_id { get; set; }
        [ForeignKey("Posts")]
        public int PostId { get; set; }
        public virtual Posts? Posts { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users? Users { get; set; }
        public DateTime created_time { get; set; }
    }
}
