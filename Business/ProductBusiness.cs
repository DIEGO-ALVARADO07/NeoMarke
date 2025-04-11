using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con productos.
    /// </summary>
    public class ProductBusiness
    {
        private readonly ProductData _productData;
        private readonly ILogger<ProductBusiness> _logger;

        public ProductBusiness(ProductData productData, ILogger<ProductBusiness> logger)
        {
            _productData = productData;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productData.GetAllAsync();
                return products.Select(MapToDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos.");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de productos", ex);
            }
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ID inválido para obtener producto: {Id}", id);
                throw new ValidationException("id", "El ID del producto debe ser mayor que cero");
            }

            try
            {
                var product = await _productData.GetByIdAsync(id);
                if (product == null)
                {
                    throw new EntityNotFoundException("product", id);
                }

                return MapToDTO(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto con ID: {Id}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el producto con ID {id}", ex);
            }
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO dto)
        {
            try
            {
                ValidateProduct(dto);

                var entity = MapToEntity(dto);
                var result = await _productData.CreateAsync(entity);

                if (result == null)
                {
                    throw new ExternalServiceException("Base de datos", "El producto creado es nulo.");
                }

                return MapToDTO(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto: {Name}", dto?.Name ?? "null");
                throw;
            }
        }

        public async Task<bool> UpdateProductAsync(ProductDTO dto)
        {
            ValidateProduct(dto);

            try
            {
                var entity = MapToEntity(dto);
                return await _productData.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto: {Id}", dto.Id);
                throw new ExternalServiceException("Base de datos", $"Error al actualizar el producto con ID {dto.Id}", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("id", "El ID del producto debe ser mayor que cero");
            }

            try
            {
                return await _productData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto con ID: {Id}", id);
                throw new ExternalServiceException("Base de datos", $"Error al eliminar el producto con ID {id}", ex);
            }
        }

        private void ValidateProduct(ProductDTO dto)
        {
            if (dto == null)
            {
                throw new ValidationException("El producto no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ValidationException("name", "El nombre del producto es obligatorio");
            }

            if (dto.Price < 0)
            {
                throw new ValidationException("price", "El precio del producto no puede ser negativo");
            }

            if (dto.Stock < 0)
            {
                throw new ValidationException("stock", "El stock del producto no puede ser negativo");
            }
        }

        private ProductDTO MapToDTO(Product entity)
        {
            return new ProductDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                Stock = entity.Stock
            };
        }

        private Product MapToEntity(ProductDTO dto)
        {
            return new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };
        }
    }
}
