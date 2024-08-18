using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qUANLYTHPT.Data;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(); // Trang chính
    }

    public async Task<IActionResult> ManageStudents()
    {
        var students = await _context.Students.ToListAsync();
        return View(students); // Truyền danh sách học sinh vào view
    }



    public async Task<IActionResult> ManageClasses()
    {
        var classes = await _context.Classes.ToListAsync();
        return View(classes);
    }





    // Các hành động CRUD khác...

    public IActionResult Error()
    {
        return View(); // Trang lỗi
    }
}
