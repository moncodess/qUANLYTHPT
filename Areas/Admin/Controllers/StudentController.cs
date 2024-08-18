using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qUANLYTHPT.Areas.Admin.Models;
using qUANLYTHPT.Data;
using System.Linq;

namespace qUANLYTHPT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danh Sách
        public IActionResult Index()
        {
            var students = _context.Students
                .Include(s => s.Class) // Bao gồm lớp liên quan
                .Include(s => s.Grades) // Bao gồm điểm liên quan
                .Select(s => new Student
                {
                    StudentId = s.StudentId,
                    FullName = s.FullName,
                    Email = s.Email,
                    Hometown = s.Hometown,
                    DateOfBirth = s.DateOfBirth,
                    ClassId = s.ClassId,
                    Class = new Class
                    {
                        ClassId = s.Class.ClassId,
                        ClassName = s.Class.ClassName,
                        Description = s.Class.Description
                    },
                    Grades = s.Grades.Select(g => new Grade
                    {
                        GradeId = g.GradeId,
                        StudentId = g.StudentId,
                        Subject = g.Subject,
                        Score = g.Score,
                        Semester = g.Semester,
                        SchoolYear = g.SchoolYear
                    }).ToList()
                }).ToList();
            return View(students);
        }

        // GET: Thêm Học Sinh
        public IActionResult Create()
        {
            ViewBag.Classes = _context.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Description = c.Description
                }).ToList();
            return View();
        }

        // POST: Thêm Học Sinh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Hometown = model.Hometown,
                    DateOfBirth = model.DateOfBirth,
                    ClassId = model.ClassId
                };
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Classes = _context.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Description = c.Description
                }).ToList();
            return View(model);
        }

        // GET: Sửa Học Sinh
        public IActionResult Edit(int id)
        {
            var student = _context.Students
                .Include(s => s.Class)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            var model = new Student
            {
                StudentId = student.StudentId,
                FullName = student.FullName,
                Email = student.Email,
                Hometown = student.Hometown,
                DateOfBirth = student.DateOfBirth,
                ClassId = student.ClassId,
                Class = new Class
                {
                    ClassId = student.Class.ClassId,
                    ClassName = student.Class.ClassName,
                    Description = student.Class.Description
                },
                Grades = student.Grades.Select(g => new Grade
                {
                    GradeId = g.GradeId,
                    StudentId = g.StudentId,
                    Subject = g.Subject,
                    Score = g.Score,
                    Semester = g.Semester,
                    SchoolYear = g.SchoolYear
                }).ToList()
            };

            ViewBag.Classes = _context.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Description = c.Description
                }).ToList();

            return View(model);
        }

        // POST: Sửa Học Sinh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                var student = _context.Students.Find(model.StudentId);
                if (student == null)
                {
                    return NotFound();
                }

                student.FullName = model.FullName;
                student.Email = model.Email;
                student.Hometown = model.Hometown;
                student.DateOfBirth = model.DateOfBirth;
                student.ClassId = model.ClassId;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Classes = _context.Classes
                .Select(c => new Class
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    Description = c.Description
                }).ToList();

            return View(model);
        }
    }
}
