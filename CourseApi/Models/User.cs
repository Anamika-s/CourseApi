using System.ComponentModel.DataAnnotations.Schema;

namespace CourseApi.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public Role? Role { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }


        //public string? RefreshToken { get; set; }


    }

    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
       
    }
}
