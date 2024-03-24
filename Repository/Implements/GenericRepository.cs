using FileUpload.API.Entities;
using FileUpload.API.Enum;
using FileUpload.API.Filters;
using FileUpload.API.Models.Common;
using FileUpload.API.Repository.Presistence;
using Microsoft.Data.SqlClient;
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

        public async Task<OperationResult> Add(T entity)
        {
            try
            {
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success("Entity added successfully.");
            }
            catch(Exception ex)
            {
                return OperationResult.Failure($"Error occured when trying to save data : {ex.Message}");
            }
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
