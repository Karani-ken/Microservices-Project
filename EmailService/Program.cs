using EmailService.Data;
using EmailService.Extensions;
using EmailService.Messaging;
using EmailService.Services;
using Microsoft.EntityFrameworkCore;

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

var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
dbContextBuilder.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
builder.Services.AddSingleton(new EmailSaveService(dbContextBuilder.Options));

builder.Services.AddSingleton<IAzureMessageBusConsumer, AzureMessageBusConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMigration();
app.useAzure();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
