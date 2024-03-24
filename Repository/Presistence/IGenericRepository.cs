using FileUpload.API.Filters;

namespace FileUpload.API.Repository.Presistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<OperationResult> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
