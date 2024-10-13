using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Repositories;
using Plantech.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PlantechContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

// Add dependency injection for services and repositories
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IHortalicaService, HortalicaService>();
builder.Services.AddScoped<IHortalicaRepository, HortalicaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Adicione isto antes do UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
