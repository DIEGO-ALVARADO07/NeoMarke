using Entity.Model;
using Entity.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;


namespace Data
{
   public class NotificationData 
   {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NotificationData> _logger;

        public NotificationData(ApplicationDbContext context, ILogger<NotificationData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear una nueva notificacion
        public async Task<Notification?> CreateAsync(Notification notification)
        {
            try
            {
                await _context.Set<Notification>().AddAsync(notification);
                await _context.SaveChangesAsync();
                return notification;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la notificaion: {ex.Message}");
                throw;
            }
        }

        // Obtener todos los movimientos
        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Set<Notification>().ToListAsync();
        }

        // Obtener notificaciones por ID de usuario
        public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
        {
            try
            {
                return await _context.Set<Notification>()
                                     .Where(n => n.IdUser == userId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener notificaciones del usuario ID {userId}: {ex.Message}");
                throw;
            }
        }


        // Actualizar un movimiento
        public async Task<bool> UpdateAsync(Notification movement)
        {
            try
            {
                _context.Set<Notification>().Update(movement);
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
                var movement = await GetByUserIdAsync(id);
                if (movement == null)
                {
                    return false;
                }
                _context.Set<Notification>().Remove(movement);
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
