﻿using Entity.Model;
using Entity.Contexts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data
{
    public class SedeData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SedeData> _logger;

        public SedeData(ApplicationDbContext context, ILogger<SedeData> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Crear una nueva sede
        public async Task<Sede?> CreateAsync(Sede sede)
        {
            try
            {
                await _context.Set<Sede>().AddAsync(sede);
                await _context.SaveChangesAsync();
                return sede;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear la sede: {ex.Message}");
                throw;
            }
        }

        // Obtener todas las sedes
        public async Task<IEnumerable<Sede>> GetAllAsync()
        {
            return await _context.Set<Sede>().ToListAsync();
        }

        // Obtener una sede por ID
        public async Task<Sede?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Sede>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener la sede con ID {id}: {ex.Message}");
                throw;
            }
        }

        // Actualizar una sede
        public async Task<bool> UpdateAsync(Sede sede)
        {
            try
            {
                _context.Set<Sede>().Update(sede);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar la sede: {ex.Message}");
                throw;
            }
        }

        // Eliminar una sede por ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var sede = await GetByIdAsync(id);
                if (sede == null)
                {
                    return false;
                }
                _context.Set<Sede>().Remove(sede);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar la sede con ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}
