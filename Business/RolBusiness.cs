using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business;

/// <summary>
/// Clase de negocio encargada de la lógica relacionada con los roles del sistema.
/// </summary>
public class RolBusiness
{
    private readonly RolData _rolData;
    private readonly ILogger _logger;

    public RolBusiness(RolData rolData, ILogger logger)
    {
        _rolData = rolData;
        _logger = logger;
    }

    // Método para obtener todos los roles como DTOs
    public async Task<IEnumerable<RolDTO>> GetAllRolesAsync()
    {
        try
        {
            var roles = await _rolData.GetAllAsync();
            return MapToDTOList(roles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los roles");
            throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de roles", ex);
        }
    }

    // Método para obtener un rol por ID como DTO
    public async Task<RolDTO> GetRolByIdAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Se intentó obtener un rol con ID inválido: {RolId}", id);
            throw new Utilities.Exceptions.ValidationException("id", "El ID del rol debe ser mayor que cero");
        }

        try
        {
            var rol = await _rolData.GetByIdAsync(id);
            if (rol == null)
            {
                _logger.LogInformation("No se encontró ningún rol con ID: {RolId}", id);
                throw new EntityNotFoundException("Rol", id);
            }

            return MapToDTO(rol);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el rol con ID: {RolId}", id);
            throw new ExternalServiceException("Base de datos", $"Error al recuperar el rol con ID {id}", ex);
        }
    }

    // Updated method to handle potential null reference for "rolCreado"
    public async Task<RolDTO> CreateRolAsync(RolDTO RolDto)
    {
        try
        {
            ValidateRol(RolDto);

            var rol = MapToEntity(RolDto);

            var rolCreado = await _rolData.CreateAsync(rol);

            // Ensure "rolCreado" is not null before mapping
            if (rolCreado == null)
            {
                _logger.LogError("El rol creado es nulo.");
                throw new ExternalServiceException("Base de datos", "Error al crear el rol: el resultado es nulo.");
            }

            return MapToDTO(rolCreado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear nuevo rol: {RolNombre}", RolDto?.NameRol ?? "null");
            throw new ExternalServiceException("Base de datos", "Error al crear el rol", ex);
        }
    }

    // Método para validar el DTO
    private void ValidateRol(RolDTO RolDto)
    {
        if (RolDto == null)
        {
            throw new Utilities.Exceptions.ValidationException("El objeto rol no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(RolDto.NameRol))
        {
            _logger.LogWarning("Se intentó crear/actualizar un rol con Name vacío");
            throw new Utilities.Exceptions.ValidationException("Name", "El Name del rol es obligatorio");
        }
    }

    // Método para mapear de Rol a RolDTO
    private RolDTO MapToDTO(Rol rol)
    {
        return new RolDTO
        {
            Id = rol.Id,
            NameRol = rol.NameRol,
            Description = rol.Description,
            Status = rol.Status
        };
    }

    // Método para mapear de RolDTO a Rol
    private static Rol MapToEntity(RolDTO rolDTO)
    {
        return new Rol
        {
            Id = rolDTO.Id,
            NameRol = rolDTO.NameRol,
            Description = rolDTO.Description,
            Status = rolDTO.Status,
            IdUser = rolDTO.IdUser,
        };
    }

    // Método para mapear una lista de Rol a una lista de RolDTO
    private IEnumerable<RolDTO> MapToDTOList(IEnumerable<Rol> roles)
    {
        var rolesDTO = new List<RolDTO>();
        foreach (var rol in roles)
        {
            rolesDTO.Add(MapToDTO(rol));
        }
        return rolesDTO;
    }
}