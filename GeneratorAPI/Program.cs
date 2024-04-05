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
    List<QuestionData> n = [new QuestionData ("���������� ������",0,true),
            new QuestionData("����� �� ������������� ��������� �������� ���������� ������?", 1, true),
            new QuestionData("����� �� ������������� ���������(��� �� �������) �������� ���������� ������?", 1, true),
            new QuestionData("��� �������� ��������� ������?", 1, true),
            new QuestionData("��� ������ � �������� ������?", 1, true),
            new QuestionData("����� �� ������������� ��������� �� �������� ���������� ������?", 1, false),
            new QuestionData("����� �� ������������� ���������(��� �� �������) �� �������� ���������� ������?", 1, false),
            new QuestionData("��� �� �������� ��������� ������?", 1, false),
            new QuestionData("��� �� ������ � �������� ������?", 1, false),
            new QuestionData("�������������� ������", 2, true),
            new QuestionData("�������������� ����", 2, true),
            new QuestionData("���������� ����", 2, true),
            new QuestionData("�������� ������������", 2, true),
            new QuestionData("��������, ���������� ������ ���������", 2, false),
            new QuestionData("�������� �����", 2, true),
            new QuestionData("��������", 2, true),
            new QuestionData("�������", 2, true),
            new QuestionData("���������� �������", 2, true),
            new QuestionData("������������ �������", 2, true),
            new QuestionData("������������ ������������ �������", 2, false),
            new QuestionData("���������� ��������", 2, false),
            new QuestionData("������������ ��������", 2, false),
            new QuestionData("�����������", 2, false),
            new QuestionData("������", 2, false),
            new QuestionData("������", 2, false),
            new QuestionData("����������� ����������", 2, false),
            new QuestionData("��������� ��� ������", 2, false),
            new QuestionData("������� ��� ���������", 2, false) ];


    db.QuestionDatas.AddRange(n);
    db.SaveChanges();
    

}

app.Run();
