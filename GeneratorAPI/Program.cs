using GeneratorAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GeneratorAPI.Models.Entities;
using GeneratorAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyBuild = WebApplication.CreateBuilder();
string connection = "Server=DESKTOP-TQLBOGP;Database=applicationdb;Trusted_Connection=True;TrustServerCertificate=True; ";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<QuestionRepository>();

var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();




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

