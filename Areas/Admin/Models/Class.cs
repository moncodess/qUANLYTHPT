namespace qUANLYTHPT.Areas.Admin.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
