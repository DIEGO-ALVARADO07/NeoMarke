using Data;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    public class SaleDetailBusiness
    {
        private readonly SaleDetailData _saleDetailData;
        private readonly ILogger<SaleDetailBusiness> _logger;

        public SaleDetailBusiness(SaleDetailData saleDetailData, ILogger<SaleDetailBusiness> logger)
        {
            _saleDetailData = saleDetailData;
            _logger = logger;
        }

        // Crear un nuevo detalle de venta
        public async Task<SaleDetailDTO> CreateAsync(SaleDetailDTO dto)
        {
            ValidateSaleDetail(dto);
            var entity = MapToEntity(dto);
            var created = await _saleDetailData.CreateAsync(entity);
            return MapToDTO(created);
        }

        // Obtener todos los detalles
        public async Task<IEnumerable<SaleDetailDTO>> GetAllAsync()
        {
            var detalles = await _saleDetailData.GetAllAsync();
            return detalles.Select(MapToDTO);
        }

        // Obtener detalles por ID de venta
        public async Task<IEnumerable<SaleDetailDTO>> GetBySaleDetailIdAsync(int idSale)
        {
            if (idSale <= 0)
                throw new ValidationException("IdSale", "El ID de la venta debe ser mayor a cero");

            var detalles = await _saleDetailData.GetBySaleDetailIdAsync(idSale);
            return detalles.Select(MapToDTO);
        }

        // Calcular total de una venta
        public async Task<decimal> CalcularTotalVentaAsync(int idSale)
        {
            var detalles = await _saleDetailData.GetBySaleIdAsync(idSale);
            return detalles.Sum(d => d.SubTotal);
        }

        // Actualizar
        public async Task<bool> UpdateAsync(SaleDetailDTO dto)
        {
            ValidateSaleDetail(dto);
            var entity = MapToEntity(dto);
            return await _saleDetailData.UpdateAsync(entity);
        }

        // Eliminar todos los detalles por ID de venta
        public async Task<bool> DeleteBySaleDetailIdAsync(int idSale)
        {
            return await _saleDetailData.DeleteBySaleDetailIdAsync(idSale);
        }

        // Validar entrada
        private void ValidateSaleDetail(SaleDetailDTO dto)
        {
            if (dto == null)
                throw new ValidationException("Detalle de venta", "El detalle no puede ser nulo");

            if (dto.IdProduct <= 0)
                throw new ValidationException("IdProduct", "Debe tener un producto válido");

            if (dto.IdSale <= 0)
                throw new ValidationException("IdSale", "Debe estar asociado a una venta válida");

            if (dto.Quantity <= 0)
                throw new ValidationException("Cantidad", "La cantidad debe ser mayor a cero");

            if (dto.SubTotal < 0)
                throw new ValidationException("Subtotal", "El subtotal no puede ser negativo");
        }

        // Mapear DTO a entidad
        private SaleDetail MapToEntity(SaleDetailDTO saleDetailDto)
        {
            return new SaleDetail
            {
                Id = saleDetailDto.Id,
                IdProduct = saleDetailDto.IdProduct,
                IdSale = saleDetailDto.IdSale,
                Quantity = saleDetailDto.Quantity,
                SubTotal = saleDetailDto.SubTotal
            };
        }

        // Mapear entidad a DTO
        private SaleDetailDTO MapToDTO(SaleDetail saleDetail)
        {
            return new SaleDetailDTO
            {
                Id = saleDetail.Id,
                IdProduct = saleDetail.IdProduct,
                IdSale = saleDetail.IdSale,
                Quantity = saleDetail.Quantity,
                SubTotal = saleDetail.SubTotal
            };
        }
    }
}
