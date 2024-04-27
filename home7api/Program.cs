using home7api;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using Core;

var builder = WebApplication.CreateBuilder(args);

//формуємо рядок підключення
string connection = builder.Configuration.GetConnectionString("DataConnection") ?? throw new InvalidOperationException("Connection string 'WebAppLibraryContext' not found.");

// Add services to the container.

builder.Services.AddDbContext(connection);

builder.Services.AddRepository();

builder.Services.AddCustomService();

builder.Services.CustomMapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//Дає доступ запитам з іншого домену
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
// Створення шляху до папки "images" в поточній робочій директорії.

if (!Directory.Exists(dir))
{
    // Перевірка, чи папка "images" не існує.

    Directory.CreateDirectory(dir);
    // Створення папки "images", якщо вона не існує.
}

app.UseStaticFiles(new StaticFileOptions
{
    // Конфігурація використання статичних файлів у додатку.

    FileProvider = new PhysicalFileProvider(dir),
    // Вказується, що фізичний провайдер файлів використовується для отримання файлів із заданого шляху.

    RequestPath = "/images"
    // Вказується, який URL-шлях буде використовуватися для доступу до статичних файлів (у цьому випадку - "/images").
});

app.SeedData();

app.UseAuthorization();

app.MapControllers();

app.Run();
