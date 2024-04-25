using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Posts
    {
        [Key]
        public int post_id { get; set; }
        public string? thumb { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? summary { get; set; }
        public int published { get; set; }
        public DateTime createdDate { get; set;}
        [ForeignKey("Users")]
        public int UserId { get; set;}
        public virtual Users? Users { get; set; }
        [ForeignKey("Tab")]
        public int? TabId { get; set; }
        public virtual Tab? Tab { get; set; }
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }       
        public virtual Category? Category { get; set; }

        public virtual ICollection<Post_Like>? Post_Likes { get; set; }
        public virtual ICollection<Post_Comment>? Post_Comments { get; set; }
    }
}
