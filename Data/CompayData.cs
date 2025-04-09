using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class CompanyData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
       
        public CompanyData(ApplicationDbContext context, ILogger<CompanyData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear una nueva empresa
        public async Task<Company?> CreateAsync(Company company)
        {
            try
            {
                await _context.Set<Company>().AddAsync(company);
                await _context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la empresa: {ex.Message}");
                throw;
            }
        }

        // Obtener todas las empresas
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Set<Company>().ToListAsync();
        }

        // Obtener una empresa por ID
        public async Task<Company?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Company>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la empresa con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar una empresa
        public async Task<bool> UpdateAsync(Company company)
        {
            try
            {
                _context.Set<Company>().Update(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la empresa: {ex.Message}");
                throw;
            }
        }

        // Eliminar una empresa por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var company = await GetByIdAsync(id);
                if (company == null)
                {
                    return false;
                }
                _context.Set<Company>().Remove(company);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la empresa con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
