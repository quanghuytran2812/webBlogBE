using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Role
    {
        [Key]
        public int role_id {get;set;}
        public string? roleName { get; set; }
        public virtual ICollection<Users>? Users { get; set; }
    }
}
