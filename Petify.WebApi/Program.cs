using Microsoft.EntityFrameworkCore;
using Petify.WebApi.Data_Model;
using Petify.WebApi;

var builder = WebApplication.CreateBuilder(args);


//Database Connection String
builder.Services.AddDbContext<PetifyDbContext>(options => options.UseSqlServer(
    builder.Configuration["ConnectionStrings:DefaultConnection"]));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
