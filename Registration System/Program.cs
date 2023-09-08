using Microsoft.EntityFrameworkCore;
using Registration_System.Data;
using Registration_System.Extensions;
using Registration_System.Models;
using Registration_System.Services;
using Registration_System.Services.IServices;
using Registration_System.Utility;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//register identity Framework
builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IJwtInterface, JwtService>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigration();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
