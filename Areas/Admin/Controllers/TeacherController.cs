using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qUANLYTHPT.Areas.Admin.Models;
using qUANLYTHPT.Data;

namespace qUANLYTHPT.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danh Sách
        public IActionResult Index()
        {
            var teachers = _context.Teachers
                .Include(t => t.Department)
                .Select(t => new Teacher
                {
                    TeacherId = t.TeacherId,
                    FullName = t.FullName,
                    Email = t.Email,
                    DepartmentId = t.DepartmentId,
                    Department = t.Department
                }).ToList();
            return View(teachers);
        }

        // GET: Thêm Giáo Viên
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments
                .Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                }).ToList();
            return View();
        }

        // POST: Thêm Giáo Viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher model)
        {
            if (ModelState.IsValid)
            {
                var teacher = new Teacher
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    DepartmentId = model.DepartmentId
                };
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _context.Departments
                .Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                }).ToList();

            return View(model);
        }

        // GET: Sửa Giáo Viên
        public IActionResult Edit(int id)
        {
            var teacher = _context.Teachers
                .Include(t => t.Department)
                .FirstOrDefault(t => t.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            var model = new Teacher
            {
                TeacherId = teacher.TeacherId,
                FullName = teacher.FullName,
                Email = teacher.Email,
                DepartmentId = teacher.DepartmentId,
                Department = teacher.Department
            };

            ViewBag.Departments = _context.Departments
                .Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                }).ToList();

            return View(model);
        }

        // POST: Sửa Giáo Viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher model)
        {
            if (ModelState.IsValid)
            {
                var teacher = _context.Teachers.Find(model.TeacherId);
                if (teacher == null)
                {
                    return NotFound();
                }

                teacher.FullName = model.FullName;
                teacher.Email = model.Email;
                teacher.DepartmentId = model.DepartmentId;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Departments = _context.Departments
                .Select(d => new Department
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName
                }).ToList();

            return View(model);
        }

        //
    }
}