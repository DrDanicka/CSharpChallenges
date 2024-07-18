using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSharpChallenge.Models
{
    /// <summary>
    /// Represents a user in the system.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Required]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required]
        [StringLength(40)]
        [DisplayName("Username:")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required]
        [StringLength(40, MinimumLength = 4)]
        [DisplayName("Password:")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [StringLength(80)]
        [DisplayName("Email:")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the user has administrative privileges.
        /// </summary>
        [Required]
        public bool Admin { get; set; }
    }
}
