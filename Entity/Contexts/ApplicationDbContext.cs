using Entity.Model;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;


namespace Entity.Contexts
{
    /// <summary>
    /// Representa el contexto de la base de datos de la aplicación, proporcionando configuraciones y métodos
    /// para la gestión de entidades y consultas personalizadas con Dapper.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Configuración de la aplicación.
        /// </summary>
        protected readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor del contexto de la base de datos.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto de base de datos.</param>
        /// <param name="configuration">Instancia de IConfiguration para acceder a la configuración de la aplicación.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configura los modelos de la base de datos aplicando configuraciones desde ensamblados.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo de base de datos.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



            /*
             * Conversión de tipos de datos Enum a string
            */


            // Conversión enum a string para el tipo de notificación
            modelBuilder.Entity<Notification>()

                .Property(n => n.TypeAction)
                .HasConversion<string>();

            // Conversión enum a string para el tipo de identificacón
            modelBuilder.Entity<Person>()

                .Property(n => n.TypeIdentification)
                .HasConversion<string>();

            // Conversión enum a string para el tipo de movimiento en inventario
            modelBuilder.Entity<MovementInventory>()

                .Property(n => n.TypeMovement)
                .HasConversion<string>();

            // Conversión enum a string para el tipo de permiso en cada rol y su accion en los formularios
            modelBuilder.Entity<RolForm>()
                .Property(n => n.Permision)
                .HasConversion<string>();



            /*
             * Relaciones entre entidades
            */

            // Metodos Relaciones entre tablas: Roles con Formularios
            modelBuilder.Entity<RolForm>()
                .HasOne(rf => rf.Rol)
                .WithMany(r => r.RolForm)
                .HasForeignKey(rf => rf.IdRol)
            .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodos Relaciones entre tablas: Formularios con Roles
            modelBuilder.Entity<RolForm>()
                .HasOne(rf => rf.Form)
                .WithMany(f => f.RolForm)
                .HasForeignKey(rf => rf.IdForm)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Metodos Relaciones entre tablas: Personas con Usuarios
            modelBuilder.Entity<Person>()
               .HasOne(p => p.User)
               .WithOne(u => u.Person)
               .HasForeignKey<User>(u => u.IdPerson) // Especifica la clave foránea
            .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            // Metodos Relaciones entre tablas: Usuarios con Roles
            modelBuilder.Entity<User>()

               .HasOne(p => p.Rol)
               .WithOne(u => u.User)
               .HasForeignKey<User>(u => u.IdRol) // Especifica la clave foránea
               .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Usuarios con Empresas
            modelBuilder.Entity<User>()
               .HasOne(p => p.Company)
               .WithMany(u => u.Users)
               .HasForeignKey(u => u.IdCompany) // Especifica la clave foránea
               .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Las Notificaiones que puede tener un Usuario
            modelBuilder.Entity<Notification>()
                .HasOne(p => p.User)
                .WithMany(u => u.Notification)
                .HasForeignKey(u => u.IdUser) // Especifica la clave foránea 
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Las Ventas que puede tener un Usuario, Solo para el caso de que el usuario sea el Administrador/Vendedor
            modelBuilder.Entity<Sale>()
                .HasOne(p => p.User)
                .WithMany(u => u.Sale)
                .HasForeignKey(u => u.IdUser) // Especifica la clave foránea
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Las Notificaiones que puede tener un Usuario
            modelBuilder.Entity<Notification>()
                .HasOne(p => p.User)
                .WithMany(u => u.Notification)
                .HasForeignKey(u => u.IdUser) // Especifica la clave foránea 
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Los Movimientos de Inventario que puede tener un Usuario(Administrador)
            modelBuilder.Entity<MovementInventory>()
                .HasOne(p => p.User)
                .WithMany(u => u.MovementInventory)
                .HasForeignKey(u => u.IdUser) // Especifica la clave foránea 
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Metodo Relaciones entre tablas: Los Movimientos de Inventario que puede tener un Inventario
            modelBuilder.Entity<MovementInventory>()
                .HasOne(p => p.Inventory)
                .WithMany(u => u.MovementInventory)
                .HasForeignKey(u => u.IdInventory) // Especifica la clave foránea 
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
        }

        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }

        /// <summary>
        /// Configura opciones adicionales del contexto, como el registro de datos sensibles.
        /// </summary>
        /// <param name="optionsBuilder">Constructor de opciones de configuración del contexto.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            // Otras configuraciones adicionales pueden ir aquí
        }

        /// <summary>
        /// Configura convenciones de tipos de datos, estableciendo la precisión por defecto de los valores decimales.
        /// </summary>
        /// <param name="configurationBuilder">Constructor de configuración de modelos.</param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        /// <summary>
        /// Guarda los cambios en la base de datos, asegurando la auditoría antes de persistir los datos.
        /// </summary>
        /// <returns>Número de filas afectadas.</returns>
        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        /// <summary>
        /// Guarda los cambios en la base de datos de manera asíncrona, asegurando la auditoría antes de la persistencia.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica si se deben aceptar todos los cambios en caso de éxito.</param>
        /// <param name="cancellationToken">Token de cancelación para abortar la operación.</param>
        /// <returns>Número de filas afectadas de forma asíncrona.</returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve una colección de resultados de tipo genérico.
        /// </summary>
        /// <typeparam name="T">Tipo de los datos de retorno.</typeparam>
        /// <param name="text">Consulta SQL a ejecutar.</param>
        /// <param name="parameters">Parámetros opcionales de la consulta.</param>
        /// <param name="timeout">Tiempo de espera opcional para la consulta.</param>
        /// <param name="type">Tipo opcional de comando SQL.</param>
        /// <returns>Una colección de objetos del tipo especificado.</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        /// <summary>
        /// Ejecuta una consulta SQL utilizando Dapper y devuelve un solo resultado o el valor predeterminado si no hay resultados.
        /// </summary>
        /// <typeparam name="T">Tipo de los datos de retorno.</typeparam>
        /// <param name="text">Consulta SQL a ejecutar.</param>
        /// <param name="parameters">Parámetros opcionales de la consulta.</param>
        /// <param name="timeout">Tiempo de espera opcional para la consulta.</param>
        /// <param name="type">Tipo opcional de comando SQL.</param>
        /// <returns>Un objeto del tipo especificado o su valor predeterminado.</returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        /// <summary>
        /// Método interno para garantizar la auditoría de los cambios en las entidades.
        /// </summary>
        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
        }

        /// <summary>
        /// Estructura para ejecutar comandos SQL con Dapper en Entity Framework Core.
        /// </summary>
        public readonly struct DapperEFCoreCommand : IDisposable
        {
            /// <summary>
            /// Constructor del comando Dapper.
            /// </summary>
            /// <param name="context">Contexto de la base de datos.</param>
            /// <param name="text">Consulta SQL.</param>
            /// <param name="parameters">Parámetros opcionales.</param>
            /// <param name="timeout">Tiempo de espera opcional.</param>
            /// <param name="type">Tipo de comando SQL opcional.</param>
            /// <param name="ct">Token de cancelación.</param>
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct
                );
            }

            /// <summary>
            /// Define los parámetros del comando SQL.
            /// </summary>
            public CommandDefinition Definition { get; }

            /// <summary>
            /// Método para liberar los recursos.
            /// </summary
            
            public void Dispose()
            {
            }
        }
    }
}