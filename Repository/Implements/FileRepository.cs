using FileUpload.API.Entities;
using FileUpload.API.Enum;
using FileUpload.API.Filters;
using FileUpload.API.Models;
using FileUpload.API.Repository.Presistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FileUpload.API.Repository.Implements
{
    public class FileRepository : GenericRepository<FileHandling>, IFileRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileRepository(AppDbContext dbContext, IWebHostEnvironment hostEnvironment) : base (dbContext)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
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

        public async Task<OperationResult> Upload(IFormFile file, string imageCode)
        {
            try
            {
                string filePath = GetFilePath(imageCode);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string imagePath = filePath + "\\" + imageCode + ".png";
                string imageName = imageCode + ".png";
                if(File.Exists(imagePath)) 
                {
                    File.Delete(imagePath);
                }
       
                using (FileStream stream = File.Create(imagePath))
                {
                    await file.CopyToAsync(stream);
                }

                var fileHandling = new FileHandling
                {
                    ImageCode = imageCode,
                    ImageName = imageName
                };

                var savedData = await Add(fileHandling);
                if(!savedData.IsSuccess)
                {
                    throw new ArgumentException();
                }

                return OperationResult.Success("File uploaded successfully.");
            }
            catch(Exception ex)
            {
                return OperationResult.Failure($"Failed to upload file : {ex.Message}");
            }   
        }

        public Task Update(IFormFile file, string imageCode)
        {
            throw new NotImplementedException();
        }

        public Task Deleted(IFormFile file, string imageCode)
        {
            throw new NotImplementedException();
        }


        private string GetFilePath(string imageCode)
        {
            return _hostEnvironment.WebRootPath + "\\Upload\\product\\" + imageCode;
        }
    }
}
