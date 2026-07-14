using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;

namespace CleanArchitectureTemplate.Application.UseCases
{
    public class ProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();

            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                FechaCreacion = p.FechaCreacion
            }).ToList();
        }

        public async Task<ProductoDTO?> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);

            if (producto == null)
            {
                return null;
            }

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                FechaCreacion = producto.FechaCreacion
            };
        }

        public async Task AddAsync(ProductoDTO productoDto)
        {
            if (string.IsNullOrEmpty(productoDto.Nombre))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío.");
            }

            if (productoDto.Precio < 0)
            {
                throw new ArgumentException("El precio del producto no puede ser negativo.");
            }

            var producto = new Producto
            {
                Nombre = productoDto.Nombre,
                Precio = productoDto.Precio,
                Descripcion = productoDto.Descripcion,
                FechaCreacion = DateTime.Now
            };

            await _productoRepository.AddAsync(producto);
        }

        public async Task UpdateAsync(ProductoDTO productoDTO)
        {
            if (string.IsNullOrEmpty(productoDTO.Nombre))
            {
                throw new ArgumentException("El nombre del producto no puede estar vacío.");
            }
            if (productoDTO.Precio < 0)
            {
                throw new ArgumentException("El precio del producto no puede ser negativo.");
            }

            var producto = new Producto
            {
                Id = productoDTO.Id,
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                FechaCreacion = DateTime.Now
            };

            await _productoRepository.UpdateAsync(producto);
        }

        public async Task DeleteAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("El ID del producto no puede ser menor o igual a cero.");   
            }
            await _productoRepository.DeleteAsync(id);
        }
    }
}
