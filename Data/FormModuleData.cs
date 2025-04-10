using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class FormModuleData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormModule> _logger;

        public FormModuleData(ApplicationDbContext context, ILogger<FormModule> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear un nuevo formulario de un modelo
        public async Task<FormModule?> CreateAsync(FormModule formModule)
        {
            try
            {
                await _context.Set<FormModule>().AddAsync(formModule);
                await _context.SaveChangesAsync();
                return formModule;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formulario: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los formularios de un modelo
        public async Task<IEnumerable<FormModule>> GetAllAsync()
        {
            return await _context.Set<FormModule>().ToListAsync();
        }

        // Obtener un formulario por ID de un modelo
        public async Task<FormModule?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<FormModule>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener el formulario con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar un formulario de un modelo
        public async Task<bool> UpdateAsync(FormModule formModule)
        {
            try
            {
                _context.Set<FormModule>().Update(formModule);
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
                var formModule = await GetByIdAsync(id);
                if (formModule == null)
                {
                    return false;
                }
                _context.Set<FormModule>().Remove(formModule);
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
