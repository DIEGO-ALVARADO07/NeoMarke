using Entity.Model;
using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Data
{
    public class SaleData
    {
     private readonly ApplicationDbContext _context;
    private readonly ILogger<SaleData> _logger;

    public SaleData(ApplicationDbContext context, ILogger<SaleData> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Crear una nueva venta
    public async Task<Sale?> CreateAsync(Sale sale)
    {
        try
        {
            await _context.Set<Sale>().AddAsync(sale);
            await _context.SaveChangesAsync();
            return sale;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al crear una venta: {ex.Message}");
            throw;
        }
    }


    // Obtener todas las ventas
    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sale
                .Include(s => s.User) // Incluye la relación con el usuario
                .Include(s => s.SaleDetail) // Incluye la relación con los detalles de la venta
            .ToListAsync();
    }

    // Obtener una venta por ID
    public async Task<Sale?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Sale
               .Include(s => s.SaleDetail) // Incluye la relación con los detalles de la venta
               .Include(s => s.User) // Incluye la relación con el usuario
               .FirstOrDefaultAsync(s => s.Id == id);
            }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener una venta con ID {id}: {ex.Message}");
            throw;
        }
    }

    // Actualizar una venta
    public async Task<bool> UpdateAsync(Sale sale)
    {
        try
        {
            _context.Set<Sale>().Update(sale);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al actualizar la venta: {ex.Message}");
            return false;
        }
    }

    // Eliminar una venta por ID
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var sale = await _context.Set<Sale>().FindAsync(id);
            if (sale == null)
                return false;

            _context.Set<Sale>().Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al eliminar una venta: {ex.Message}");
            return false;
        }
    }

}

}

