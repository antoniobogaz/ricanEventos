
using ricanEventos.Repositories;
using ricanEventos.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();
builder.Services.AddRazorPages()
                    .AddSessionStateTempDataProvider();
// builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews()
                    .AddSessionStateTempDataProvider();

var app = builder.Build();

app.UseSession();
app.UseMiddleware<AuthMiddleware>();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
