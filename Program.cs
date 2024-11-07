using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Plantech;
using Plantech.Data;
using Plantech.Interfaces;
using Plantech.Repositories;
using Plantech.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));

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
//DI Usuario
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//DI Fornecedores
builder.Services.AddScoped<IFornecedoresRepository, FornecedoresRepository>();
builder.Services.AddScoped<IFornecedorService, FornecedoreService>();
//DI Clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService,ClienteService>();
//DI Insumos
builder.Services.AddScoped<IInsumoRepository, InsumoRepository>();
builder.Services.AddScoped<IInsumoService, InsumoService>();
//DI Hortalica
builder.Services.AddScoped<IHortalicaService, HortalicaService>();
builder.Services.AddScoped<IHortalicaRepository, HortalicaRepository>();
//DI Plantio
builder.Services.AddScoped<IPlantioRepository, PlantioRepository>();
builder.Services.AddScoped<IPlantioService, PlantioService>();
//DI Ordens de Compra
builder.Services.AddScoped<IOrdensCompraRepository, OrdensCompraRepository>();
builder.Services.AddScoped<IOrdensCompraService, OrdensCompraService>();
//DI  Insumo da comra
builder.Services.AddScoped<IInsumoCompraRepository, InsumoCompraRepository>();
builder.Services.AddScoped<IInsumoCompraService, InsumoCompraService>();
//DI Lotes Insumo
builder.Services.AddScoped<ILotesInsumosRepository, LotesInsumosRepository>();
builder.Services.AddScoped<ILotesInsumosService, LotesInsumosService>();
//DI Lotes Hortalicas
builder.Services.AddScoped<ILotesHortalicasRepository, LotesHortalicasRepository>();
builder.Services.AddScoped<ILotesHortalicasService, LotesHortalicasService>();
//DI Plantio
builder.Services.AddScoped<IPlantioRepository, PlantioRepository>();
builder.Services.AddScoped<IPlantioService, PlantioService>();
//DI Colheitas
builder.Services.AddScoped<IColheitaRepository, ColheitaRepository>();
builder.Services.AddScoped<IColheitaService, ColheitaService>();
//DI Funcionarios
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IFuncionarioService, FuncionarioService>();
//DI Vendas
builder.Services.AddScoped<IVendasRepository, VendasRepository>();
builder.Services.AddScoped<IVendasService, VendasService>();


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
