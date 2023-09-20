using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Ticket.Data.ViewModels
{
    public class LoginVM
    {
        [Required, Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
