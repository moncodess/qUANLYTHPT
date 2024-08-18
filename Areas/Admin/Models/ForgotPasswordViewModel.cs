using System.ComponentModel.DataAnnotations;

namespace qUANLYTHPT.Areas.Admin.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
