using home7api;
using Infrastructure;
using Microsoft.Extensions.FileProviders;
using Core;

var builder = WebApplication.CreateBuilder(args);

//������� ����� ����������
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

//�� ������ ������� � ������ ������
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
// ��������� ����� �� ����� "images" � ������� ������� ��������.

if (!Directory.Exists(dir))
{
    // ��������, �� ����� "images" �� ����.

    Directory.CreateDirectory(dir);
    // ��������� ����� "images", ���� ���� �� ����.
}

app.UseStaticFiles(new StaticFileOptions
{
    // ������������ ������������ ��������� ����� � �������.

    FileProvider = new PhysicalFileProvider(dir),
    // ���������, �� �������� ��������� ����� ��������������� ��� ��������� ����� �� �������� �����.

    RequestPath = "/images"
    // ���������, ���� URL-���� ���� ����������������� ��� ������� �� ��������� ����� (� ����� ������� - "/images").
});

app.SeedData();

app.UseAuthorization();

app.MapControllers();

app.Run();
