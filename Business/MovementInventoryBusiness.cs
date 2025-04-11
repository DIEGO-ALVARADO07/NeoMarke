using Data;
using Entity.DTOs;
using Entity.Enum;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    public class MovementInventoryBusiness
    {
        private readonly MovementInventoryData _movementInventoryData;
        private readonly ILogger<MovementInventoryBusiness> _logger;

        public MovementInventoryBusiness(MovementInventoryData movementInventoryData, ILogger<MovementInventoryBusiness> logger)
        {
            _movementInventoryData = movementInventoryData;
            _logger = logger;
        }

        // Crear un nuevo movimiento de inventario
        public async Task<MovementInventoryDTO> CreateAsync(MovementInventoryDTO dto)
        {
            ValidateMovementInventory(dto);
            var entity = MapToEntity(dto);
            var created = await _saleDetailData.CreateAsync(entity);
            return MapToDTO(created);
        }

        // Obtener todos los movimientos del inventario
        public async Task<IEnumerable<MovementInventoryDTO>> GetAllAsync()
        {
            var detalles = await _movementInventoryData.GetAllAsync();
            return detalles.Select(MapToDTO);
        }


        // Obtener detalles por ID de los movimientos en el inventario
        public async Task<IEnumerable<MovementInventoryDTO>> GetByMovementInventoryIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("IdSale", "El ID de movimiento de inventario debe ser mayor a cero");

            var detalles = await _movementInventoryData.GetByMovementInventoryIdAsync(id);
            return detalles.Select(MapToDTO);
        }

        // Actualizar Movimientos de Inventario 
        public async Task<bool> UpdateMovementInventoryAsync(ProductDTO dto)
        {
            ValidateMovementInventory(dto);

            try
            {
                var entity = MapToEntity(dto);
                return await _movementInventoryData.UpdateMovementInventoryAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar movimiento de inventario: {Id}", dto.Id);
                throw new ExternalServiceException("Base de datos", $"Error al actualizar el movimiento de inventario con ID {dto.Id}", ex);
            }
        }

        // Eliminar un movimiento de inventario por Id
        public async Task<bool> DeleteMovementInventaryAsync(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("id", "El ID del movimiento debe ser mayor que cero");
            }

            try
            {
                return await _movementInventoryData.DeleteMovementInventaryAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar movimiento de inventario con ID: {Id}", id);
                throw new ExternalServiceException("Base de datos", $"Error al eliminar el movimiento de inventario con ID {id}", ex);
            }
        }

        // Validaciones de los movimientos de inventario
        private void ValidateMovementInventory(MovementInventoryDTO dto)
        {
            if (dto == null) { 
                throw new BusinessException("El DTO de movimiento de inventario no puede ser nulo.");
            }

            if (dto.Quantity <= 0) { 
                throw new BusinessException("La cantidad debe ser mayor que cero.");
            }

            if (string.IsNullOrWhiteSpace(dto.Description)) { 
                throw new BusinessException("La descripción es obligatoria.");
            }

            if (Enum.IsDefined(dto.TypeMovement)) { 
                throw new BusinessException("El tipo de movimiento es inválido.");
            }
            if (dto.IdInventory <= 0 || dto.IdProduct <= 0 || dto.IdUser <= 0) { 
                throw new BusinessException("Debe proporcionar IDs válidos para inventario, producto y usuario.");
            }
        }

        private MovementInventoryDTO MapToDTO(MovementInventory entity)
        {
            return new MovementInventoryDTO
            {
                Id = entity.Id,
                TypeMovement = entity.TypeMovement,
                Quantity = entity.Quantity,
                Date = entity.Date,
                Description = entity.Description,
                IdInventory = entity.IdInventory,
                IdProduct = entity.IdProduct,
                IdUser = entity.IdUser
            };
        }

        private MovementInventory MapToEntity(MovementInventoryDTO dto)
        {
            return new MovementInventory
            {
                Id = dto.Id,
                TypeMovement = dto.TypeMovement,
                Quantity = dto.Quantity,
                Date = dto.Date,
                Description = dto.Description,
                IdInventory = dto.IdInventory,
                IdProduct = dto.IdProduct,
                IdUser = dto.IdUser
            };
        }
    }
}
