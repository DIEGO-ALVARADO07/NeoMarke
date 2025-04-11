using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Utilities.Constants;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con las notificaciones realizadas a cada usuario en el sistema.
    /// </summary>
    public class NotificationBusiness
    {
        private readonly NotificationData _notificationData;
        private readonly UserData _userData;
        private readonly ILogger<NotificationBusiness> _logger;

        public NotificationBusiness(NotificationData notificationData, UserData userData, ILogger<NotificationBusiness> logger)
        {
            _notificationData = notificationData;
            _userData = userData;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todas las notificaciones del sistema.
        /// </summary>
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

        /// <summary>
        /// Obtener notificaciones de un usuario específico por su ID.
        /// </summary>
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

        // Updated method to handle potential null values for the "created" object.
        public async Task<NotificationDTO> CreateNotificationAsync(NotificationDTO notificationDto)
        {
            try
            {
                ValidateNotification(notificationDto);

                var entity = MapToEntity(notificationDto);
                var created = await _notificationData.CreateAsync(entity);

                if (created == null)
                {
                    throw new ExternalServiceException("Base de datos", "La notificación creada es nula.");
                }

                return MapToDTO(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear notificación para usuario ID: {UserId}", notificationDto.IdUser);
                throw new ExternalServiceException("Base de datos", "Error al crear la notificación", ex);
            }
        }

        /// <summary>
        /// Enviar notificación a todos los administradores.
        /// </summary>
        public async Task<bool> NotifyAdminsOnSaleAsync(string message)
        {
            var admins = await _userData.GetByRolNameAsync(RolNames.Administrador);

            foreach (var admin in admins)
            {
                var noti = new NotificationDTO
                {
                    IdUser = admin.Id,
                    Mensage = message,
                    Date = DateTime.Now,
                    Read = false,
                    TypeAction = Entity.Enum.TypeAction.Sale
                };

                await CreateNotificationAsync(noti);
            }

            return true;
        }

        /// <summary>
        /// Notifica a los administradores cuando se realiza una venta.
        /// </summary>
        public async Task<bool> NotificationSale(int idVendedor, string nombreProducto)
        {
            var mensaje = $"Se ha realizado una venta del producto '{nombreProducto}' por el usuario ID: {idVendedor}.";
            return await NotifyAdminsOnSaleAsync(mensaje);
        }

        /// <summary>
        /// Notifica a los administradores cuando se realiza una compra.
        /// </summary>

        public async Task<bool> NotificationBuyOut(int idComprador, string nombreProducto)
        {
            var mensaje = $"El usuario ID: {idComprador} ha realizado una compra del producto '{nombreProducto}'.";
            return await NotifyAdminsOnSaleAsync(mensaje);
        }

        /// <summary>
        // Notifica al propio comprador sobre su compra.
        /// </summary>
        
        public async Task<bool> NotificationBuyer(int idComprador, string nombreProducto)
        {
            if (idComprador <= 0 || string.IsNullOrWhiteSpace(nombreProducto))
                throw new ValidationException("NotificaciónCompra", "Datos inválidos para notificar al comprador.");

            var mensaje = $"Gracias por tu compra del producto '{nombreProducto}'. ¡Esperamos que lo disfrutes!";

            var noti = new NotificationDTO
            {
                IdUser = idComprador,
                Mensage = mensaje,
                Date = DateTime.Now,
                Read = false,
                TypeAction = Entity.Enum.TypeAction.Buy
            };

            await CreateNotificationAsync(noti);
            return true;
        }


        // Validación básica
        private void ValidateNotification(NotificationDTO notificationDto)
        {
            if (notificationDto == null)
                throw new ValidationException("Notificación", "La notificación no puede ser nula");

            if (string.IsNullOrWhiteSpace(notificationDto.Mensage))
                throw new ValidationException("Mensaje", "El mensaje es obligatorio");

            if (notificationDto.IdUser <= 0)
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

        // Mapear de DTO a Entity
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
