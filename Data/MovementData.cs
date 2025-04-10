using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
   public class MovementData 
   {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MovementData> _logger;

        public MovementData(ApplicationDbContext context, ILogger<MovementData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo movimiento
        public async Task<Movement?> CreateAsync(Movement movement)
        {
            try
            {
                await _context.Set<Movement>().AddAsync(movement);
                await _context.SaveChangesAsync();
                return movement;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el movimiento: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los movimientos
        public async Task<IEnumerable<Movement>> GetAllAsync()
        {
            return await _context.Set<Movement>().ToListAsync();
        }

        // Obtener un movimiento por ID
        public async Task<Movement?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Movement>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el movimiento con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un movimiento
        public async Task<bool> UpdateAsync(Movement movement)
        {
            try
            {
                _context.Set<Movement>().Update(movement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el movimiento: {ex.Message}");
                throw;
            }
        }

        // Eliminar un movimiento por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var movement = await GetByIdAsync(id);
                if (movement == null)
                {
                    return false;
                }
                _context.Set<Movement>().Remove(movement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el movimiento con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
