using Entity.Model;
using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class SaleDetailData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SaleDetailData> _logger;

        public SaleDetailData(ApplicationDbContext context, ILogger<SaleDetailData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo detalle de venta
        public async Task<SaleDetail> CreateAsync(SaleDetail saleDetail)
        {
            try
            {
                await _context.SaleDetail.AddAsync(saleDetail);
                await _context.SaveChangesAsync();
                return saleDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear un detalle de venta: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los detalles de las ventas
        public async Task<IEnumerable<SaleDetail>> GetAllAsync()
        {
            return await _context.SaleDetail
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .ToListAsync();
        }

        // Obtener todos los detalles de una venta específica por su ID
        public async Task<IEnumerable<SaleDetail>> GetBySaleDetailIdAsync(int idSale)
        {
            try
            {
                return await _context.SaleDetail
                    .Where(sd => sd.IdSale == idSale)
                    .Include(sd => sd.Product)
                    .Include(sd => sd.Sale)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los detalles de venta para la venta ID {idSale}: {ex.Message}");
                throw;
            }
        }


        // Actualizar un detalle de venta
        public async Task<bool> UpdateAsync(SaleDetail saleDetail)
        {
            try
            {
                _context.SaleDetail.Update(saleDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el detalle de venta: {ex.Message}");
                return false;
            }
        }

        // Eliminar todos los detalles de una venta específica por su ID
        public async Task<bool> DeleteBySaleDetailIdAsync(int idSale)
        {
            try
            {
                var detalles = await _context.SaleDetail
                    .Where(sd => sd.IdSale == idSale)
                    .ToListAsync();

                if (!detalles.Any())
                    return false;

                _context.SaleDetail.RemoveRange(detalles);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar los detalles de la venta ID {idSale}: {ex.Message}");
                return false;
            }
        }
    }
}
