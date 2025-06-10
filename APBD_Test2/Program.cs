using APBD_Test2.Services;
using APBD_Test2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using IPublishingHouseService = APBD_Test2.Services.Interfaces.IPublishingHouseService;

var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(cfg.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IPublishingHouseService, PublishingHouseService>();
builder.Services.AddScoped<IBookService, BookService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//dotnet ef migrations add <MigrationName>