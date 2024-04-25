using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string? thumb { get; set; }
        public string? email_address { get; set; }
        public string? fullName { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? salt { get; set; }
        public int active { get; set; }
        public DateTime created_time { get; set; }
        public DateTime lastLogin { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Posts>? Posts { get; set; }
        public virtual ICollection<Post_Like>? Post_Likes { get; set; }
        public virtual ICollection<Post_Comment>? Post_Comments { get; set; }
    }
}
