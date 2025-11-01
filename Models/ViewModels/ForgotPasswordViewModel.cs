using System.ComponentModel.DataAnnotations;

namespace LogiDriverPortal.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
