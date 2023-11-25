using System.ComponentModel.DataAnnotations;

namespace Fab.ViewModels.AccountFolder
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
