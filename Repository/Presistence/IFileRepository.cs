using FileUpload.API.Filters;
using FileUpload.API.Models;

namespace FileUpload.API.Repository.Presistence
{
    public interface IFileRepository : IGenericRepository<FileHandling>
    {
        Task<IEnumerable<FileHandling>> AllFiles();
        Task<FileHandling> GetByID(int id);
        Task<FileHandling> GetByCode(string imageCode);
        Task<OperationResult> Upload (IFormFile file, string imageCode);
        Task Update (IFormFile file, string imageCode);
        Task Deleted (IFormFile file, string imageCode);
    }
}
