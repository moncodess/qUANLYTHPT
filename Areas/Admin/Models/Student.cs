namespace qUANLYTHPT.Areas.Admin.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Hometown { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
