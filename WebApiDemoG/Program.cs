using Microsoft.EntityFrameworkCore;
using WebApiDemoG.Data;
using WebApiDemoG.Formatters;
using WebApiDemoG.Repositories.Abstract;
using WebApiDemoG.Repositories.Concrete;
using WebApiDemoG.Services.Abstract;
using WebApiDemoG.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new TextCsvOutputFornatter());
    options.InputFormatters.Add(new TextCsvInputFormatter());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<StudentDBContext>(opt =>
{
    opt.UseSqlServer(connection);
});
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

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
