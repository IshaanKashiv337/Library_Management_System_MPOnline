using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class LoginModel
    {
        [Key]
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
