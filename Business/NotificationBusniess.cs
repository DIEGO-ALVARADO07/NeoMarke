using Data;
using Entity.DTOs;
using Entity.Enum;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using System.Linq;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con las notificaciones realizadas a cada usuario en el sistema.
    /// </summary>
    public class NotificationBusiness
    {
        private readonly NotificationData _notificationData;
        private readonly ILogger<NotificationBusiness> _logger;

        public NotificationBusiness(NotificationData notificationData, ILogger<NotificationBusiness> logger)
        {
            _notificationData = notificationData;
            _logger = logger;
        }

        // Obtener todas las notificaciones como DTO
        public async Task<IEnumerable<NotificationDTO>> GetAllNotificationsAsync()
        {
            try
            {
                var notifications = await _notificationData.GetAllAsync();
                return notifications.Select(MapToDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las notificaciones.");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de notificaciones", ex);
            }
        }

        // Obtener notificaciones por usuario
        public async Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(int IdUser)
        {
            if (IdUser <= 0)
                throw new ValidationException("IdUsuario", "El ID del usuario debe ser mayor a cero");

            try
            {
                var notifications = await _notificationData.GetByUserIdAsync(IdUser);
                return notifications.Select(MapToDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las notificaciones del usuario ID: {UserId}", IdUser);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar notificaciones para el usuario ID {IdUser}", ex);
            }
        }

        // Crear una nueva notificación
        public async Task<NotificationDTO> CreateNotificationAsync(NotificationDTO dto)
        {
            try
            {
                ValidateNotification(dto);

                var entity = MapToEntity(dto);
                var created = await _notificationData.CreateAsync(entity);

                return MapToDTO(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificación para usuario ID: {UserId}", dto.IdUser);
                throw new ExternalServiceException("Base de datos", "Error al crear la notificación", ex);
            }
        }

        // Validación básica
        private void ValidateNotification(NotificationDTO dto)
        {
            if (dto == null)
                throw new ValidationException("Notificación", "La notificación no puede ser nula");

            if (string.IsNullOrWhiteSpace(dto.Mensage))
                throw new ValidationException("Mensaje", "El mensaje es obligatorio");

            if (dto.IdUser <= 0)
                throw new ValidationException("IdUsuario", "El ID del usuario debe ser mayor a cero");
        }

        // Mapear de Entity a DTO
        private NotificationDTO MapToDTO(Notification notification)
        {
            return new NotificationDTO
            {
                Id = notification.Id,
                IdUser = notification.IdUser,
                Mensage = notification.Mensage,
                Date = notification.Date,
                Read = notification.Read,
                IdReference = notification.IdReference,
                TypeAction = notification.TypeAction
            };
        }

        // Reemplazar la línea problemática en el método MapToEntity
        private Notification MapToEntity(NotificationDTO notificationDto)
        {
            return new Notification
            {
                Id = notificationDto.Id,
                IdUser = notificationDto.IdUser,
                Mensage = notificationDto.Mensage,
                Date = notificationDto.Date,
                Read = notificationDto.Read,
                IdReference = notificationDto.IdReference,
                TypeAction = notificationDto.TypeAction 
            };
        }
    }
}
