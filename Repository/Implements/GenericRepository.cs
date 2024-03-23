using FileUpload.API.Entities;
using FileUpload.API.Enum;
using FileUpload.API.Models.Common;
using FileUpload.API.Repository.Presistence;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.API.Repository.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            if (entity is BaseModel baseEntity)
            {
                baseEntity.SetRowStatus(EnumTypes.InActive);
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
