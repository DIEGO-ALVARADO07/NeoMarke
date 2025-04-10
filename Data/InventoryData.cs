using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class InventoryData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Inventory> _logger;

        public InventoryData(ApplicationDbContext context, ILogger<Inventory> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo inventario
        public async Task<Inventory?> CreateAsync(Inventory inventoryItem)
        {
            try
            {
                await _context.Set<Inventory>().AddAsync(inventoryItem);
                await _context.SaveChangesAsync();
                return inventoryItem;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el inventario: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los inventarios
        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Set<Inventory>().ToListAsync();
        }

        // Obtener un inventario por ID
        public async Task<Inventory?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Inventory>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el inventario con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un inventario
        public async Task<bool> UpdateAsync(Inventory inventoryItem)
        {
            try
            {
                _context.Set<Inventory>().Update(inventoryItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el inventario: {ex.Message}");
                throw;
            }
        }
    }
}
