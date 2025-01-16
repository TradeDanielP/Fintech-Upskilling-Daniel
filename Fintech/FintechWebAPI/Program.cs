using FintechWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using FintechWebAPI.Services;
using FintechWebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<AccountRepository>();

// Register services
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<AccountService>();

// Agregar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Agregar servicios de controladores
builder.Services.AddControllers();

// Register DbContext
builder.Services.AddDbContext<FintechDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
/* app.UseAuthorization(); */
app.MapControllers();

app.Run();

