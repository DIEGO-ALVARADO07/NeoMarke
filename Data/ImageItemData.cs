using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class ImageItemData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ImageItem> _logger;

        public ImageItemData(ApplicationDbContext context, ILogger<ImageItem> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear una nueva imagen
        public async Task<ImageItem?> CreateAsync(ImageItem image)
        {
            try
            {
                await _context.Set<ImageItem>().AddAsync(image);
                await _context.SaveChangesAsync();
                return image;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la imagen: {ex.Message}");
                throw;
            }
        }

        // Obtener todas las imágenes
        public async Task<IEnumerable<ImageItem>> GetAllAsync()
        {
            return await _context.Set<ImageItem>().ToListAsync();
        }

        // Obtener una imagen por ID
        public async Task<ImageItem?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<ImageItem>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la imagen con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar una imagen por ID
        public async Task<bool> UpdateAsync(ImageItem image)
        {
            try
            {
                _context.Set<ImageItem>().Update(image);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la imagen: {ex.Message}");
                throw;
            }
        }
    }
}
