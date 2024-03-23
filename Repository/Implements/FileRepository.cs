using FileUpload.API.Entities;
using FileUpload.API.Enum;
using FileUpload.API.Models;
using FileUpload.API.Repository.Presistence;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.API.Repository.Implements
{
    public class FileRepository : GenericRepository<FileHandling>, IFileRepository
    {
        private readonly AppDbContext _dbContext;

        public FileRepository(AppDbContext dbContext) : base (dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<FileHandling>> AllFiles()
        {
            return await _dbContext.FileHandlings.AsNoTracking()
                           .Where(a => a.RowStatus == (short)EnumTypes.Active)
                           .ToListAsync();
        }

        public async Task<FileHandling?> GetByCode(string imageCode)
        {
            return await _dbContext.FileHandlings.AsNoTracking()
               .FirstOrDefaultAsync(a => a.ImageCode == imageCode && a.RowStatus == (short)EnumTypes.Active);
        }

        public async Task<FileHandling?> GetByID(int id)
        {
            return await _dbContext.FileHandlings.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id && a.RowStatus == (short)EnumTypes.Active);
        }
    }
}
