using System;
using Entity
namespace Data
{
    ///<summary>
    ///Actualiza un rol existente en la base de datos.
    /// </summary>
    /// <param name="rol">objeto con la informacion actualizada</param>
    /// <returns>True si la operacion fue exitosa, False en caso contrario</returns>
    public async Task<bool> UpdateAAsync(Rol rol)
{
    try
    {
        _context.Set<Rol>().Update(rol);
        await _context.SaveChangesAsync();
        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error al actualizar el rol: {ex.Message}");
        return false;
    }
}

///<summary>
///Crea un nuevo rol en labase de datos.
/// </summary>
/// <param name="rol">Instancia del rol a crear.</param>
/// <returns>El rol creado.</returns>
public async Task<Rol>CreateAsync(Rol rol)
{
    try
    {
        await _context.Set<Rol>.AddAsync(rol);
        await _context.SaveChsngesAsync();
        return rol;
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error al crear el rol: {ex.Message}");
        throw;
    }
}

///<summary>
///Obtiene todos los roles almacenados en la base de datos.
///</summary>
///<returns>Lista de roles.</returns>
public async Task<IEnumerable<rol>> GetAllAsync()
{
    return await _context.Set<Rol>().ToListAsync();
}

public async Task<Rol?> GetByIdAsync(int id)
{
    try
    {
        return await _cotext.Set<Rol>().FindAsync(id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error al obtener rol con ID {RolId}", id);
        throw; //Re-lanza la excepcion ´para q sea manejadas en capas superiores
    }
}


    ///<summary>
    ///Repositorio encargado de la gestión de la entidad Rol en la base de datos.
    ///</summary>
    public class RolData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        ///<summary>
        ///Constructor que recibe el contexto de base de datos.
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos.</param>
        ///<param name="logger">Instancia de <see cref="ILogger"/> para el registro de logs.</param>
        public RolData(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        ///<summary>
        ///Actualiza un rol existente en la base de datos.
        /// </summary>
        /// <param name="rol">Objeto con la información actualizada.</param>
        /// <returns>True si la operación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> UpdateAsync(Rol rol)
        {
            try
            {
                _context.Set<Rol>().Update(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol: {ex.Message}");
                return false;
            }
        }

        ///<summary>
        ///Crea un nuevo rol en la base de datos.
        /// </summary>
        /// <param name="rol">Instancia del rol a crear.</param>
        /// <returns>El rol creado.</returns>
        public async Task<Rol> CreateAsync(Rol rol)
        {
            try
            {
                await _context.Set<Rol>().AddAsync(rol);
                await _context.SaveChangesAsync();
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rol: {ex.Message}");
                throw;
            }
        }

        ///<summary>
        ///Obtiene todos los roles almacenados en la base de datos.
        ///</summary>
        ///<returns>Lista de roles.</returns>
        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Set<Rol>().ToListAsync();
        }

        ///<summary>
        ///Obtiene un rol por su ID.
        ///</summary>
        ///<param name="id">ID del rol.</param>
        ///<returns>El rol encontrado o null si no existe.</returns>
        public async Task<Rol?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Rol>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener rol con ID {RolId}", id);
                throw; //Re-lanza la excepción para que sea manejada en capas superiores
            }
        }

        ///<summary>
        ///Elimina un rol de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del rol a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa, False en caso contrario.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rol = await _context.Set<Rol>().FindAsync(id);
                if (rol == null)
                    return false;

                _context.Set<Rol>().Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el rol: {ex.Message}");
                return false;
            }
        }
    }
}

///<summary>
///elimina un rol de la base de datos
/// </summary>
/// <param name="id">identificador unico del rol a eliminar</param>
/// <returns>True si la eliminacion fue exitosa, false caso contrario</returns>
public async Task<bool> DeleteAsync(int id)
{
    try
    {
        var rol = await _context.Set<Rol>().FindAsync(id);
        if (rol == null)
            return false;

        _context.Set<Rol>().Remove(rol);
        await _context.SaveChangesAsync();
        return true;
    }
catch  (Exception ex)
    {
        Console.WriteLine($"Error al eliminr el rol:{ex.Message}");
        return false;
    }
}