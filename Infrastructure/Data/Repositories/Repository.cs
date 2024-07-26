using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data.Factories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T>
    {

        protected readonly SqlConnectionFactory _sqlConnection;

        protected Repository(SqlConnectionFactory sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public async Task CreateAsync(string storedProcedure,object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                storedProcedure,
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteAsync(string storedProcedure, object param)
        {
            try
            {
                using var connection = _sqlConnection.CreateConnection();
                await connection.ExecuteAsync(
                    storedProcedure,
                    param,
                    commandType: CommandType.StoredProcedure
                ).ConfigureAwait(false); // Configura el await para no capturar el contexto de sincronización.
            }
            catch (Exception ex)
            {
                // Log o maneja la excepción según sea necesario
                throw new ApplicationException($"Error ejecutando el procedimiento almacenado {storedProcedure}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll(string storedProcedure)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryAsync<T>(
                storedProcedure,
                commandType: CommandType.StoredProcedure);
        }

        public virtual async Task<T?> GetById(string storedProcedure,object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(
                storedProcedure,
                param,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateAsync(string storedProcedure, object param)
        {
            using var connection = _sqlConnection.CreateConnection();
            await connection.ExecuteAsync(
                storedProcedure,
                param,
                commandType: CommandType.StoredProcedure);
        }
    }
}
