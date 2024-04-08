﻿using GeneratorAPI.Models;
using GeneratorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GeneratorAPI.Repositories
{
    public class QuestionDataRepository
    {
        private readonly AppDbContext _dbContext;

            public QuestionDataRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<QuestionDataEntity>> Get()
        {
            return await _dbContext.QuestionDatas.AsNoTracking().ToListAsync();    
        }

        public async Task<List<QuestionDataEntity>> GetWithImages()
        {
            return await _dbContext.QuestionDatas.AsNoTracking().Include(q=>q.images).ToListAsync();
        }

        public async Task<QuestionDataEntity?> GetById(Guid id)
        {
            return await _dbContext.QuestionDatas.AsNoTracking().FirstOrDefaultAsync(q=>q.id==id);
        }

        public async Task<List<QuestionDataEntity>> GetByFilter(int type)
        {
            var query = _dbContext.QuestionDatas.AsNoTracking();
            query = query.Where(q => q.type == type);
            return await query.ToListAsync();
        }

        public async Task Add(QuestionDataEntity questionData)
        {
            await _dbContext.AddAsync(questionData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<QuestionDataEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.QuestionDatas.AsNoTracking().Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        }


    }
}