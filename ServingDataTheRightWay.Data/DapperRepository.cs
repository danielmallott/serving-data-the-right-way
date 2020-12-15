using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ServingDataTheRightWay.Data.Models;

namespace ServingDataTheRightWay.Data
{
    public class DapperRepository
    {
        private readonly IConfiguration _configuration;

        private IDbConnection Connection => new SqlConnection(this._configuration["ConnectionString:default"]);

        public DapperRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #region Hardcoded SQL (Don't Do This)

        public async Task<City> GetCityById(int cityId)
        {
            using var connection = Connection;
            return await connection.QuerySingleOrDefaultAsync<City>(
                $"SELECT CityID as ID, CityName, StateProvinceID, LatestRecordedPopulation, LastEditedBy, ValidFrom, ValidTo FROM Application.Cities WHERE CityID = {cityId}");
        }

        #endregion

        #region Stored Procedure - Doing Better

        public async Task<IEnumerable<City>> SearchCities(string cityName)
        {
            var parameter = new { cityName = cityName };

            using var connection = Connection;
            return await connection.QueryAsync<City>("dbo.SearchCities", parameter,
                commandType: CommandType.StoredProcedure);
        }

        #endregion

        #region Generated SQL - Some People Are Okay With This

        // Only possible for simple CRUD with an extra addon.

        #endregion
    }
}
