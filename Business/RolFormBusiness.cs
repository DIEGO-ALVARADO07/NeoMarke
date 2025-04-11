using Data;
using Entity.DTOs;
using Entity.Enum;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los roles de formulario en el sistema.
    /// </summary>
    public class RolFormBusiness
    {
        private readonly RolFormData _rolFormData;
        private readonly ILogger<RolFormBusiness> _logger;

        public RolFormBusiness(RolFormData rolFormData, ILogger<RolFormBusiness> logger)
        {
            _rolFormData = rolFormData;
            _logger = logger;
        }

        // Método para obtener todos los roles de formulario como DTOs
        public async Task<IEnumerable<RolFormDTO>> GetAllRolFormsAsync()
        {
            try
            {
                var rolForms = await _rolFormData.GetAllAsync();
                return MapToDTOList(rolForms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los roles de formulario");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de roles de formulario", ex);
            }
        }

        // Método para obtener un rol de formulario por ID como DTO
        public async Task<RolFormDTO> GetRolFormByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un rol de formulario con ID inválido: {Id}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del rol de formulario debe ser mayor que cero");
            }

            try
            {
                var rolForm = await _rolFormData.GetByIdAsync(id);
                if (rolForm == null)
                {
                    _logger.LogInformation("No se encontró ningún rol de formulario con ID: {Id}", id);
                    throw new EntityNotFoundException("rolForm", id);
                }

                return MapToDTO(rolForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol de formulario con ID: {Id}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el rol de formulario con ID {id}", ex);
            }
        }

        // Método para crear un rol de formulario desde un DTO
        public async Task<RolFormDTO> CreateRolFormAsync(RolFormDTO rolFormDto)
        {
            try
            {
                ValidateRolForm(rolFormDto);

                var rolForm = MapToEntity(rolFormDto);

                var rolFormCreado = await _rolFormData.CreateAsync(rolForm);

                if (rolFormCreado == null)
                {
                    _logger.LogWarning("El rol de formulario creado es nulo.");
                    throw new ExternalServiceException("Base de datos", "El rol de formulario creado es nulo.");
                }

                return MapToDTO(rolFormCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo rol de formulario: {Name}", rolFormDto?.Permision.ToString() ?? "null");

                throw new ExternalServiceException("Base de datos", "Error al crear el rol de formulario", ex);
            }
        }

        // Método para actualizar un rol de formulario
        public async Task<RolFormDTO> UpdateRolFormAsync(RolFormDTO rolFormDto)
        {
            try
            {
                ValidateRolForm(rolFormDto);

                var existing = await _rolFormData.GetByIdAsync(rolFormDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("rolForm", rolFormDto.Id);

                var updated = MapToEntity(rolFormDto);
                var success = await _rolFormData.UpdateAsync(updated);

                if (!success)
                    throw new ExternalServiceException("Base de datos", "No se pudo actualizar el rol de formulario");

                return rolFormDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol de formulario con ID: {Id}", rolFormDto?.Id);
                throw;
            }
        }

        // Método para eliminar un rol de formulario por ID
        public async Task<bool> DeleteRolFormByIdAsync(int id)
        {
            try
            {
                var exists = await _rolFormData.GetByIdAsync(id);
                if (exists == null)
                    throw new EntityNotFoundException("rolForm", id);

                return await _rolFormData.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol de formulario con ID: {Id}", id);
                throw;
            }
        }

        // Método para validar el DTO
        private void ValidateRolForm(RolFormDTO rolFormDto)
        {
            if (rolFormDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto RolForm no puede ser nulo");
            }

            if (!Enum.IsDefined(typeof(Permision), rolFormDto.Permision))
            {
                _logger.LogWarning("Se intentó crear/actualizar rol con permiso inválido: {Permision}", rolFormDto.Permision);
                throw new Utilities.Exceptions.ValidationException("El permiso es obligatorio o inválido");
            }

        }

        // Método para mapear de RolForm a RolFormDto
        private RolFormDTO MapToDTO(RolForm rolForm)
        {
            return new RolFormDTO
            {
                Id = rolForm.Id,
                Permision = rolForm.Permision,
                IdRol = rolForm.IdRol,
                IdForm = rolForm.IdForm,
            };
        }

        // Método para mapear de RolFormDto a RolForm
        private RolForm MapToEntity(RolFormDTO rolFormDto)
        {
            return new RolForm
            {
                Id = rolFormDto.Id,
                Permision = rolFormDto.Permision,
                IdRol = rolFormDto.IdRol,
                IdForm = rolFormDto.IdForm
            };
        }

        // Método para mapear una lista de RolForm a una lista de RolFormDto
        private IEnumerable<RolFormDTO> MapToDTOList(IEnumerable<RolForm> rolForms)
        {
            return rolForms.Select(MapToDTO).ToList();
        }
    }
}
