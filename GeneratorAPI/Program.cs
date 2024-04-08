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
//string connection = "Server=DESKTOP-TQLBOGP;Database=applicationdb;Trusted_Connection=True;TrustServerCertificate=True; ";
string connection = "Server=DESKTOP-TQLBOGP;Database=applicationdb;userid=Vitya;password=1234;Trusted_Connection=True;TrustServerCertificate=True; ";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

var app = builder.Build();


app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/questions", async (AppDbContext db) => await db.QuestionDatas.ToListAsync());


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

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
var options = optionsBuilder
  // .UseSqlServer(@"Server=(localdb)\VIC1;Database=chepel;Trusted_Connection=True;")
  .UseSqlServer(connection).Options;
QuestionDataRepository q = new QuestionDataRepository(new AppDbContext(options));
var qd = new QuestionDataEntity();
qd.text = "Что лучше: гурьянова или лапко";
qd.type = 1;
qd.flag = false;
qd.probability = 1;
await q.Add(qd);
