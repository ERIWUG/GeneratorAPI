using GeneratorAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




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
using (var db = new AppDbContext())
{
    List<QuestionData> n = [new QuestionData ("элементами дороги",0,true),
            new QuestionData("Какие из перечисленных элементов являются элементами дороги?", 1, true),
            new QuestionData("Какие из перечисленных элементов(при их наличии) являются элементами дороги?", 1, true),
            new QuestionData("Что является элементом дороги?", 1, true),
            new QuestionData("Что входит в элементы дороги?", 1, true),
            new QuestionData("Какие из перечисленных элементов не являются элементами дороги?", 1, false),
            new QuestionData("Какие из перечисленных элементов(при их наличии) не являются элементами дороги?", 1, false),
            new QuestionData("Что не является элементом дороги?", 1, false),
            new QuestionData("Что не входит в элементы дороги?", 1, false),
            new QuestionData("Разделительные полосы", 2, true),
            new QuestionData("Разделительные зоны", 2, true),
            new QuestionData("Трамвайные пути", 2, true),
            new QuestionData("Островки безопасности", 2, true),
            new QuestionData("Островки, выделенные только разметкой", 2, false),
            new QuestionData("Проезжие части", 2, true),
            new QuestionData("Тротуары", 2, true),
            new QuestionData("Обочины", 2, true),
            new QuestionData("Пешеходные дорожки", 2, true),
            new QuestionData("Велосипедные дорожки", 2, true),
            new QuestionData("Обособленные велосипедные дорожки", 2, false),
            new QuestionData("Пешеходные переходы", 2, false),
            new QuestionData("Велосипедные переезды", 2, false),
            new QuestionData("Перекрестки", 2, false),
            new QuestionData("Кюветы", 2, false),
            new QuestionData("Обрезы", 2, false),
            new QuestionData("Придорожные насаждения", 2, false),
            new QuestionData("Кустарник при дороге", 2, false),
            new QuestionData("Дорожки для всадников", 2, false) ];


    db.QuestionDatas.AddRange(n);
    db.SaveChanges();
    

}

app.Run();
