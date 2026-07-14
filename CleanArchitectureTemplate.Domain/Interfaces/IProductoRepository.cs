using CleanArchitectureTemplate.Domain.Entities;

namespace CleanArchitectureTemplate.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();

        Task<Producto?> GetByIdAsync(int id);

        Task<Producto?> GetByNameAsync(string nombre);

        Task AddAsync(Producto producto);

        Task UpdateAsync(Producto producto);

        Task DeleteAsync(int id);
    }
}
