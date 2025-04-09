using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con las personas del sistema.
    /// </summary>
    public class PersonBusniess
    {
        private readonly PersonData _personData;
        private readonly ILogger _logger;

        public PersonBusniess(PersonData personData, ILogger logger)
        {
            _personData = personData;
            _logger = logger;
        }

        // Método para obtener todos las personas como DTOs
        public async Task<IEnumerable<PersonDTO>> GetAllPersonsAsync()
        {
            try
            {
                var persons = await _personData.GetAllAsync();
                return MapToDTOList(persons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las sedes");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de sedes", ex);
            }
        }

        // Método para obtener un usuario por ID como DTO
        public async Task<PersonDTO> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener una persona con ID inválido: {PersonId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID de la persona debe ser mayor que cero");
            }

            try
            {
                var person = await _personData.GetByIdAsync(id);
                if (person == null)
                {
                    _logger.LogInformation("No se encontró ninguna Persona con ID: {PersonId}", id);
                    throw new EntityNotFoundException("Person", id);
                }

                return MapToDTO(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID: {PersonId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el usuario con ID {id}", ex);
            }
        }

        // Método para crear un usuario desde un DTO
        public async Task<PersonDTO> CreateUserAsync(PersonDTO personDTO)
        {
            try
            {
                ValidatePerson(personDTO);

                var Person = MapToEntity(personDTO);

                var PersonCreado = await _personData.CreateAsync(Person);

                return MapToDTO(PersonCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo usuario: {FirstName}", personDTO?.FirstName ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el usuario", ex);
            }
        }

        // Método para validar el DTO
        private void ValidatePerson(PersonDTO personDto)
        {
            if (personDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto usuario no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(personDto.FirstName))
            {
                _logger.LogWarning("Se intentó crear/actualizar un usuario con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Nombre", "El nombre de la persona es obligatorio");
            }

            if (string.IsNullOrWhiteSpace(personDto.Email))
            {
                _logger.LogWarning("Se intentó crear/actualizar una persona con correo vacío");
                throw new Utilities.Exceptions.ValidationException("Email", "El correo de la persona es obligatorio");
            }
        }

        // Método para mapear de User a UserDto
        private static PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                TypeIdentification = person.TypeIdentification,
                NumberIdentification = person.NumberIdentification,
                Status = person.Status,
                IdUser = person.IdUser
            };
        }

        // Método para mapear de UserDto a User
        private static Person MapToEntity(PersonDTO personDto)
        {
            return new Person
            {
                Id = personDto.Id,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                PhoneNumber = personDto.PhoneNumber,
                Email = personDto.Email,
                TypeIdentification = personDto.TypeIdentification,
                NumberIdentification = personDto.NumberIdentification,
                Status = personDto.Status,
                IdUser = personDto.IdUser
            };
        }

        // Método para mapear una lista de User a una lista de UserDto
        private static IEnumerable<PersonDTO> MapToDTOList(IEnumerable<Person> persons)
        {
            var personsDTO = new List<PersonDTO>();
            foreach (var person in persons)
            {
                personsDTO.Add(MapToDTO(person));
            }
            return personsDTO;
        }
    }
}