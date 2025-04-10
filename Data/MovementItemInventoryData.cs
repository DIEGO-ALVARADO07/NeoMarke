using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class MovementItemInventoryData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovementItemInventory> _logger;

        public MovementItemInventoryData(ApplicationDbContext context, ILogger<MovementItemInventory> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo movimiento de inventario
        public async Task<MovementItemInventory?> CreateAsync(MovementItemInventory movementItemInventory)
        {
            try
            {
                await _context.Set<MovementItemInventory>().AddAsync(movementItemInventory);
                await _context.SaveChangesAsync();
                return movementItemInventory;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el movimiento de inventario: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los movimientos de inventario
        public async Task<IEnumerable<MovementItemInventory>> GetAllAsync()
        {
            return await _context.Set<MovementItemInventory>().ToListAsync();
        }

        // Obtener un movimiento de inventario por ID
        public async Task<MovementItemInventory?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<MovementItemInventory>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el movimiento de inventario con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un movimiento de inventario
        public async Task<bool> UpdateAsync(MovementItemInventory movementItemInventory)
        {
            try
            {
                _context.Set<MovementItemInventory>().Update(movementItemInventory);
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
                var movementItemInventory = await GetByIdAsync(id);
                if (movementItemInventory == null)
                {
                    return false;
                }
                _context.Set<MovementItemInventory>().Remove(movementItemInventory);
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
