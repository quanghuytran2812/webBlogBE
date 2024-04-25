using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class UserDTO
    {
        public int user_id { get; set; }
        public string? thumb { get; set; }
        public string? email_address { get; set; }
        public string? fullName { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public string? salt { get; set; }
        public int? active { get; set; }
        public DateTime? created_time { get; set; }
        public DateTime? lastLogin { get; set; }
        public int? RoleId { get; set; }
    }
}
