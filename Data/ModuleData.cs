using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class ModuleData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModuleData> _logger;
        public ModuleData(ApplicationDbContext context, ILogger<ModuleData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo módulo
        public async Task<ModuleData?> CreateAsync(ModuleData module)
        {
            try
            {
                await _context.Set<ModuleData>().AddAsync(module);
                await _context.SaveChangesAsync();
                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el módulo: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los módulos
        public async Task<IEnumerable<ModuleData>> GetAllAsync()
        {
            return await _context.Set<ModuleData>().ToListAsync();
        }

        // Obtener un módulo por ID
        public async Task<ModuleData?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<ModuleData>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el módulo con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un módulo
        public async Task<bool> UpdateAsync(ModuleData module)
        {
            try
            {
                _context.Set<ModuleData>().Update(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el módulo: {ex.Message}");
                throw;
            }
        }

        // Eliminar un módulo por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var module = await GetByIdAsync(id);
                if (module == null)
                {
                    return false;
                }
                _context.Set<ModuleData>().Remove(module);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el módulo con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
