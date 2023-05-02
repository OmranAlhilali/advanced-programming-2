using Data;
using Microsoft.EntityFrameworkCore;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EFContext>(
options =>
    options.UseSqlServer(
           "Server=.;Database=Advanced_Programming_DB;Trusted_Connection=true;TrustServerCertificate=True"
          ));

builder.Services.AddTransient<StudentRepository>();
builder.Services.AddTransient<DepartmentRepository>();

builder.Services.AddTransient<SubjectRepository>();
builder.Services.AddTransient<SubjectLectureRepository>();

builder.Services.AddTransient<ExamRepository>();
builder.Services.AddTransient<StudentMarkRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
