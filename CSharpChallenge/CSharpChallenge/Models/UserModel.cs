using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSharpChallenge.Models
{
    public class UserModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        [StringLength(40)]
        [DisplayName("Username:")]
        public string UserName { get; set; }
        [Required]
        [StringLength(40, MinimumLength =4)]
        [DisplayName("Password:")]
        public string Password { get; set; }
        [Required]
        [StringLength(80)]
        [DisplayName("Email:")]
        public string Email { get; set; }
        [Required]
        public bool  Admin { get; set; }
    }
}
