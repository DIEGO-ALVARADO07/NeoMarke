using Data;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de manejar la lógica relacionada con las ventas.
    /// </summary>
    public class SaleBusiness
    {
        private readonly SaleData _saleData;
        private readonly ILogger<SaleBusiness> _logger;

        public SaleBusiness(SaleData saleData, ILogger<SaleBusiness> logger)
        {
            _saleData = saleData;
            _logger = logger;
        }

        /// <summary>
        /// Crea una nueva venta.
        /// </summary>
        public async Task<SaleDTO> CreateAsync(SaleDTO saleDto)
        {
            try
            {
                ValidateSale(saleDto);
                var sale = MapToEntity(saleDto);
                var created = await _saleData.CreateAsync(sale);

                if (created == null) // Asegurandonos de que lo creado no sea nulo antes de pasarlo a MapToDTO
                {
                    throw new ExternalServiceException("Base de datos", "La creación de la venta devolvió un resultado nulo.");
                }

                return MapToDTO(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la venta.");
                throw new ExternalServiceException("Base de datos", "Error al crear la venta", ex);
            }
        }

        /// <summary>
        /// Obtiene todas las ventas.
        /// </summary>
        public async Task<IEnumerable<SaleDTO>> GetAllAsync()
        {
            try
            {
                var sales = await _saleData.GetAllAsync();
                return sales.Select(MapToDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas.");
                throw new ExternalServiceException("Base de datos", "Error al obtener las ventas", ex);
            }
        }

        /// <summary>
        /// Obtiene una venta por ID.
        /// </summary>
        public async Task<SaleDTO?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("Id", "El ID debe ser mayor a cero.");

            try
            {
                var sale = await _saleData.GetByIdAsync(id);
                return sale == null ? null : MapToDTO(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la venta con ID {id}.");
                throw new ExternalServiceException("Base de datos", $"Error al obtener la venta ID {id}", ex);
            }
        }

        /// <summary>
        /// Elimina una venta por ID.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("Id", "El ID debe ser mayor a cero.");

            try
            {
                return await _saleData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la venta con ID {id}.");
                throw new ExternalServiceException("Base de datos", $"Error al eliminar la venta ID {id}", ex);
            }
        }

        /// <summary>
        /// Valida los campos de la venta.
        /// </summary>
        private void ValidateSale(SaleDTO saleDto)
        {
            if (saleDto == null)
                throw new ValidationException("Venta", "La venta no puede ser nula");

            if (saleDto.Date == default)
                throw new ValidationException("Comprador", "El ID del comprador debe ser válido");

            if (saleDto.Totaly <= 0)
                throw new ValidationException("Vendedor", "El ID del vendedor debe ser válido");

            if (saleDto.IdUser <= 0)
                throw new ValidationException("Total", "El total de la venta debe ser mayor a cero");

            if (saleDto.IdSaleDetail <= 0)
                throw new ValidationException("Total", "El total de la venta debe ser mayor a cero");
        }

        /// <summary>
        /// Mapea de entidad a DTO.
        /// </summary>
        private SaleDTO MapToDTO(Sale sale)
        {
            return new SaleDTO
            {
                Id = sale.Id,
                Date = sale.Date,
                Totaly = sale.Totaly,
                IdUser = sale.IdUser,
                IdSaleDetail = sale.IdSaleDetail
            };
        }

        /// <summary>
        /// Mapea de DTO a entidad.
        /// </summary>
        private Sale MapToEntity(SaleDTO dto)
        {
            return new Sale
            {
                Id = dto.Id,
                Date = dto.Date,
                Totaly = dto.Totaly,
                IdUser = dto.IdUser,
                IdSaleDetail = dto.IdSaleDetail
            };
        }
    }
}
