using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los usuarios del sistema.
    /// </summary>
    public class UserBusiness
    {
        private readonly UserData _userData;
        private readonly ILogger _logger;

        public UserBusiness(UserData userData, ILogger logger)
        {
            _userData = userData;
            _logger = logger;
        }

        // Método para obtener todos los usuarios como DTOs
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userData.GetAllAsync();
                return MapToDTOList(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de usuarios", ex);
            }
        }

        // Método para obtener un usuario por ID como DTO
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un usuario con ID inválido: {UserId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del usuario debe ser mayor que cero");
            }

            try
            {
                var user = await _userData.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogInformation("No se encontró ningún usuario con ID: {UserId}", id);
                    throw new EntityNotFoundException("User", id);
                }

                return MapToDTO(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID: {UserId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el usuario con ID {id}", ex);
            }
        }

        // Método para crear un usuario desde un DTO
        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                ValidateUser(userDTO);

                var User = MapToEntity(userDTO);

                var UserCreado = await _userData.CreateAsync(User);

                return MapToDTO(UserCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo usuario: {UserName}", userDTO?.UserName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el usuario", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto usuario no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(userDto.UserName))
            {
                _logger.LogWarning("Se intentó crear/actualizar un usuario con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Nombre", "El nombre del usuario es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(userDto.Password))
            {
                _logger.LogWarning("Se intentó crear/actualizar un usuario con contraseña vacío");
                throw new Utilities.Exceptions.ValidationException("Password", "La contraseña del usuario es obligatorio");
            }
        }

        // Método para mapear de User a UserDto
        private UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Status = user.Status,
                IdPerson = user.IdPerson,
                IdRol = user.IdRol,
                IdCompany = user.IdCompany
            };
        }

        // Método para mapear de UserDto a User
        private User MapToEntity(UserDTO userDto)
        {
            return new User
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Status = userDto.Status,
                IdPerson = userDto.IdPerson,
                IdRol = userDto.IdRol,
                IdCompany = userDto.IdCompany
            };
        }

        // Método para mapear una lista de User a una lista de UserDto
        private IEnumerable<UserDTO> MapToDTOList(IEnumerable<User> users)
        {
            var usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                usersDTO.Add(MapToDTO(user));
            }
            return usersDTO;
        }
    }
}