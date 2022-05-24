using System.ComponentModel.DataAnnotations;

namespace RetroShirt.API.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Inappropriate username entry.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Inappropriate password entry.")]
        public string Password { get; set; }
    }
}
