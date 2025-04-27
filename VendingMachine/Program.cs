using Microsoft.EntityFrameworkCore;
using Serilog;
using VendingMachine.Models;
using VendingMachine.Repositories;
using VendingMachine.Services;

var builder = WebApplication.CreateBuilder(args);

// Логирование необработанных исключений
AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
{
    var logPath = Path.Combine(Directory.GetCurrentDirectory(), "fatal_error.log");
    File.AppendAllText(logPath, $"[{DateTime.Now}] {args.ExceptionObject}\n");
};

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine("Logs", "error.log"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Добавление сервисов
builder.Services.AddControllersWithViews();

// Добавление контекста базы данных
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IOrderService, OrderService>();



var app = builder.Build();

// Конфигурация HTTP-конвейера
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");

// Инициализация базы данных начальными данными
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}

app.Run();