using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using qUANLYTHPT.Data;
using qUANLYTHPT.Areas.Admin.Models;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Cấu hình Entity Framework Core với SQL Server
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        // Thêm các dịch vụ cho MVC
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Cấu hình middleware
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Giaodien/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        // Cấu hình middleware xác thực và phân quyền
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // Định nghĩa tuyến đường cho khu vực (area)
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            // Định nghĩa tuyến đường mặc định
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Giaodien}/{action=Index}/{id?}"
            );
        });

        // Cung cấp dữ liệu khởi tạo
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                // Không cần phải cung cấp dữ liệu cho UserManager nếu không sử dụng Identity
                // var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                // await SeedData.Initialize(services, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Đã xảy ra lỗi khi cung cấp dữ liệu vào cơ sở dữ liệu.");
            }
        }

        app.Run();
    }
}
