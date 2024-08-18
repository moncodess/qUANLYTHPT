namespace qUANLYTHPT.Areas.Admin.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } // <--- Add this navigation property
        public string Subject { get; set; }
        public float Score { get; set; }
        public string Semester { get; set; }
        public string SchoolYear { get; set; }
    }
}
