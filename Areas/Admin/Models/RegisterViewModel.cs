using System.ComponentModel.DataAnnotations;

namespace qUANLYTHPT.Areas.Admin.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Vui lòng nhập Họ.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Tên.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại Mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [Compare("Password", ErrorMessage = "Mật khẩu và Mật khẩu nhập lại không khớp.")]
        public string ConfirmPassword { get; set; }
    }
}

