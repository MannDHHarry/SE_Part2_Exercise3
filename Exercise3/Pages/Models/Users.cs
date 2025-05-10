using System.ComponentModel.DataAnnotations;

namespace Exercise3.Pages.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Lock { get; set; }
    }
}
