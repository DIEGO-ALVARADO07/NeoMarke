using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Entity;

namespace Data
{
    public class BuyOutData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BuyOut> _logger;

        public BuyOutData(ApplicationDbContext context, ILogger<BuyOut> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo formulario de un modelo
        public async Task<BuyOut?> CreateAsync(BuyOut buyOut)
        {
            try
            {
                await _context.Set<BuyOut>().AddAsync(buyOut);
                await _context.SaveChangesAsync();
                return buyOut;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formulario: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los formularios de un modelo
        public async Task<IEnumerable<BuyOut>> GetAllAsync()
        {
            return await _context.Set<BuyOut>().ToListAsync();
        }

        // Obtener un formulario por ID de un modelo
        public async Task<BuyOut?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<BuyOut>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el formulario con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un formulario de un modelo
        public async Task<bool> UpdateAsync(BuyOut buyOut)
        {
            try
            {
                _context.Set<BuyOut>().Update(buyOut);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el formulario: {ex.Message}");
                throw;
            }
        }

        // Eliminar un formulario de un modelo por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var buyOut = await GetByIdAsync(id);
                if (buyOut == null)
                {
                    return false;
                }
                _context.Set<BuyOut>().Remove(buyOut);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el formulario con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
