using Entity.Model;
using Entity.Contexts;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class MovementInventoryData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovementInventory> _logger;

        public MovementInventoryData(ApplicationDbContext context, ILogger<MovementInventory> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo movimiento de inventario
        public async Task<MovementInventory?> CreateAsync(MovementInventory movementInventory)
        {
            try
            {
                await _context.Set<MovementInventory>().AddAsync(movementInventory);
                await _context.SaveChangesAsync();
                return movementInventory;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el movimiento de inventario: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los movimientos de inventario
        public async Task<IEnumerable<MovementInventory>> GetAllAsync()
        {
            return await _context.Set<MovementInventory>().ToListAsync();
        }

        // Obtener un movimiento de inventario por ID
        public async Task<MovementInventory?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<MovementInventory>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el movimiento de inventario con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un movimiento de inventario
        public async Task<bool> UpdateAsync(MovementInventory movementInventory)
        {
            try
            {
                _context.Set<MovementInventory>().Update(movementInventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el movimiento de inventario: {ex.Message}");
                throw;
            }
        }

        // Eliminar un movimiento de inventario por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var movementInventory = await GetByIdAsync(id);
                if (movementInventory == null)
                {
                    return false;
                }
                _context.Set<MovementInventory>().Remove(movementInventory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el movimiento de inventario: {ex.Message}");
                throw;
            }
        }
    }
}
