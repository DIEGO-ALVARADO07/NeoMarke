using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los formularios del sistema.
    /// </summary>
    public class FormBusniess
    {
        private readonly FormData _formData;
        private readonly ILogger _logger;

        public FormBusniess(FormData formData, ILogger logger)
        {
            _formData = formData;
            _logger = logger;
        }

        // Método para obtener todos los formularios como DTOs
        public async Task<IEnumerable<FormDTO>> GetAllFormsAsync()
        {
            try
            {
                var forms = await _formData.GetAllAsync();
                return MapToDTOList(forms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los formularios");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de formularios", ex);
            }
        }

        // Método para obtener un formulario por ID como DTO
        public async Task<FormDTO> GetFormByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un formulario con ID inválido: {FormId}", id);
                throw new ValidationException("id", "El ID del formulario debe ser mayor que cero");
            }

            try
            {
                var form = await _formData.GetByIdAsync(id);
                if (form == null)
                {
                    _logger.LogInformation("No se encontró ningún formulario con ID: {FormId}", id);
                    throw new EntityNotFoundException("Form", id);
                }

                return MapToDTO(form);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el formulario con ID: {FormId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el formulario con ID {id}", ex);
            }
        }

        // Método para crear un usuario desde un DTO
        public async Task<FormDTO> CreateFormAsync(FormDTO formDTO)
        {
            try
            {
                ValidateForm(formDTO);

                var Form = MapToEntity(formDTO);
                var FormCreado = await _formData.CreateAsync(Form);

                return MapToDTO(FormCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo usuario: {UserName}", formDTO?.NameForm ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el usuario", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateForm(FormDTO formDto)
        {
            {
                if (formDto == null)
                {
                    throw new ValidationException("El objeto sede no puede ser nulo");
                }

                if (string.IsNullOrWhiteSpace(formDto.NameForm))
                {
                    _logger.LogWarning("Se intentó crear/actualizar una sede con Name vacío");
                    throw new ValidationException("Name", "El Name de la sede es obligatorio");
                }
            }
        }

        // Método para mapear de Form a FormDto
        private FormDTO MapToDTO(Form form)
        {
            return new FormDTO
            {
                Id = form.Id,
                NameForm = form.NameForm,
                Description = form.Description,
                Status = form.Status,
                IdRolForm = form.IdRolForm
            };
        }

        // Método para mapear de FormDto a Form
        private Form MapToEntity(FormDTO formDto)
        {
            return new Form
            {
                Id = formDto.Id,
                NameForm = formDto.NameForm,
                Description = formDto.Description,
                Status = formDto.Status,
                IdRolForm = formDto.IdRolForm
            };
        }

        // Método para mapear una lista de Form a una lista de FormDto
        private IEnumerable<FormDTO> MapToDTOList(IEnumerable<Form> forms)
        {
            var formsDTO = new List<FormDTO>();
            foreach (var user in forms)
            {
                formsDTO.Add(MapToDTO(user));
            }
            return formsDTO;
        }
    }
}