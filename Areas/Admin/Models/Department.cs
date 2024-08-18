using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace qUANLYTHPT.Areas.Admin.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "Tên khoa")]
        public string DepartmentName { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}
