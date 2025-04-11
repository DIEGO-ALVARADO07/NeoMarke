using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business;

/// <summary>
/// Clase de negocio encargada de la lógica relacionada con las sedes del sistema.
/// </summary>
public class SedeBusiness
{
    private readonly SedeData _sedeData;
    private readonly ILogger _logger;

    public SedeBusiness(SedeData sedeData, ILogger logger)
    {
        _sedeData = sedeData;
        _logger = logger;
    }

    // Método para obtener todas las sedes como DTOs
    public async Task<IEnumerable<SedeDTO>> GetAllSedesAsync()
    {
        try
        {
            var sedes = await _sedeData.GetAllAsync();
            return MapToDTOList(sedes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todas las sedes");
            throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de sedes", ex);
        }
    }

    // Método para obtener una sede por ID como DTO
    public async Task<SedeDTO> GetSedeByIdAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Se intentó obtener una sede con ID inválido: {SedeId}", id);
            throw new ValidationException("id", "El ID de la sede debe ser mayor que cero");
        }

        try
        {
            var sede = await _sedeData.GetByIdAsync(id);
            if (sede == null)
            {
                _logger.LogInformation("No se encontró ninguna sede con ID: {SedeId}", id);
                throw new EntityNotFoundException("Sede", id);
            }

            return MapToDTO(sede);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener la sede con ID: {SedeId}", id);
            throw new ExternalServiceException("Base de datos", $"Error al recuperar la sede con ID {id}", ex);
        }
    }

    // Updated method to handle potential null reference for "sedeCreada"
    public async Task<SedeDTO> CreateSedeAsync(SedeDTO sedeDto)
    {
        try
        {
            ValidateSede(sedeDto);

            var sede = MapToEntity(sedeDto);

            var sedeCreada = await _sedeData.CreateAsync(sede);

            if (sedeCreada == null)
            {
                _logger.LogError("La creación de la sede devolvió un resultado nulo");
                throw new ExternalServiceException("Base de datos", "Error al crear la sede: resultado nulo");
            }

            return MapToDTO(sedeCreada);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear nueva sede: {SedeNombre}", sedeDto?.NameSede ?? "null");
            throw new ExternalServiceException("Base de datos", "Error al crear la sede", ex);
        }
    }

    // Método para validar el DTO
    private void ValidateSede(SedeDTO sedeDto)
    {
        if (sedeDto == null)
        {
            throw new ValidationException("El objeto sede no puede ser nulo");
        }

        if (string.IsNullOrWhiteSpace(sedeDto.NameSede))
        {
            _logger.LogWarning("Se intentó crear/actualizar una sede con Name vacío");
            throw new ValidationException("Name", "El Name de la sede es obligatorio");
        }
    }

    // Método para mapear de Sede a SedeDto
    private SedeDTO MapToDTO(Sede sede)
    {
        return new SedeDTO
        {
            Id = sede.Id,
            NameSede = sede.NameSede,
            AddressSede = sede.AddressSede,
            PhoneSede = sede.PhoneSede,
            EmailSede = sede.EmailSede,
            CodeSede = sede.CodeSede,
            Status = sede.Status,
            IdCompany = sede.IdCompany
        };
    }

    // Método para mapear de SedeDto a Sede
    private Sede MapToEntity(SedeDTO sedeDto)
    {
        return new Sede
        {
            Id = sedeDto.Id,
            NameSede = sedeDto.NameSede,
            CodeSede = sedeDto.CodeSede,
            AddressSede = sedeDto.AddressSede,
            EmailSede = sedeDto.EmailSede,
            Status = sedeDto.Status,
            IdCompany = sedeDto.IdCompany
        };
    }

    // Método para mapear una lista de Sede a una lista de SedeDto
    private IEnumerable<SedeDTO> MapToDTOList(IEnumerable<Sede> sedes)
    {
        var sedesDTO = new List<SedeDTO>();
        foreach (var sede in sedes)
        {
            sedesDTO.Add(MapToDTO(sede));
        }
        return sedesDTO;
    }
}