using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class CategoryData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryData> _logger;

        public CategoryData(ApplicationDbContext context, ILogger<CategoryData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear una nueva categoría
        public async Task<Category?> CreateAsync(Category category)
        {
            try
            {
                await _context.Set<Category>().AddAsync(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la categoría: {ex.Message}");
                throw;
            }
        }

        // Obtener todas las categorías
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        // Obtener una categoría por ID
        public async Task<Category?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Category>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la categoría con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar una categoría
        public async Task<bool> UpdateAsync(Category category)
        {
            try
            {
                _context.Set<Category>().Update(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la categoría: {ex.Message}");
                throw;
            }
        }

        // Eliminar una categoría por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await GetByIdAsync(id);
                if (category == null)
                {
                    return false;
                }
                _context.Set<Category>().Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la categoría con ID {id}: {ex.Message}");
                throw;
            }
        }

    }
}
