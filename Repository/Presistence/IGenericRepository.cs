namespace FileUpload.API.Repository.Presistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
