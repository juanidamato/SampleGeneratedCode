
using SampleGeneratedCodeApplication.Commons.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace SampleGeneratedCodeInfrastructure
{
    public class SQLDatabaseHelper : IDatabaseHelper
    {
        private readonly IConfiguration _config;

        public SQLDatabaseHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetArrayDataAsync<T,U>( string command, U parameters)
        {
            using (IDbConnection db = new SqlConnection(_config.GetConnectionString("Default")))
            {
                return await db.QueryAsync<T>(command, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //public async Task DoCommandAsync(dynamic config, string command, dynamic parameters)
        //{

        //}
    }
}
