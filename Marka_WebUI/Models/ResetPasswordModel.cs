using System.ComponentModel.DataAnnotations;

namespace Marka_WebUI.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
