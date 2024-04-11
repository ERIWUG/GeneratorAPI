﻿using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace GeneratorAPI.Repositories
{
    public class AnswerRepository
    {
        private readonly AppDbContext _dbContext;

            public AnswerRepository(AppDbContext context)
        {
            _dbContext = context;
        }

         public async Task<List<AnswerEntity>> Get()
         {
             return await _dbContext.Answers.AsNoTracking().ToListAsync();    
         }
        /*
         public async Task<List<QuestionEntity>> GetWithImages()
         {
             return await _dbContext.Questions.AsNoTracking().Include(q=>q.Images).ToListAsync();
         }

        









         public async Task<List<QuestionEntity>> GetByPage(int page, int pageSize)
         {
             return await _dbContext.Questions.AsNoTracking().Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
         }*/
        public async Task Add(string text, int theme, decimal par)
        {
            var answerData = new AnswerEntity
            {
                Text = text,
                ThemeEntity = _dbContext.Themes.Where(c => c.Id == theme).ElementAt(0),
                Par = par,

            };
            await _dbContext.AddAsync(answerData);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<AnswerEntity?> GetById(int id)
        {
            return await _dbContext.Answers.Include(u => u.ThemeEntity).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
