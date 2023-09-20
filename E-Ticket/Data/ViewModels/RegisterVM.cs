using System.ComponentModel.DataAnnotations;

namespace E_Ticket.Data.ViewModels
{
    public class RegisterVM
    {
        [Required, Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Display(Name = "Confirm Password"), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
