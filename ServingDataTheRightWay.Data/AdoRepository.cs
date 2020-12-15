using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ServingDataTheRightWay.Data.Models;

namespace ServingDataTheRightWay.Data
{
    public class AdoRepository
    {
        private readonly IConfiguration _configuration;

        private SqlConnection Connection => new SqlConnection(this._configuration["ConnectionString:default"]);

        public AdoRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #region Hardcoded SQL - Don't Do This!

        public async Task<City> GetCityById(int cityId)
        {
            var query =
                $"SELECT CityID as ID, CityName, StateProvinceID, LatestRecordedPopulation, LastEditedBy, ValidFrom, ValidTo FROM Application.Cities WHERE CityID = {cityId}";
            await using var connection = Connection;
            await using var sqlCommand = new SqlCommand(query, connection);

            await connection.OpenAsync();
            await using var reader = await sqlCommand.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return null;
            }

            var result = new City();
            // Note there could be a subtle bug if you are filtering on a column that's not unique.
            while (await reader.ReadAsync())
            {
                result.Id = Convert.ToInt32(reader["ID"]);
                result.CityName = Convert.ToString(reader["CityName"]);
                result.StateProvinceId = Convert.ToInt32(reader["StateProvinceID"]);
                result.LatestRecordedPopulation = await reader.IsDBNullAsync("LatestRecordedPopulation")
                    ? default(long?)
                    : Convert.ToInt64(reader["LatestRecordedPopulation"]);
                result.LastEditedBy = Convert.ToInt32(reader["LastEditedBy"]);
                result.ValidFrom = Convert.ToDateTime(reader["ValidFrom"]);
                result.ValidTo = Convert.ToDateTime(reader["ValidTo"]);
            }

            return result;
        }

        #endregion

        #region Stored Procedure - Doing Better

        public async Task<IEnumerable<City>> SearchCities(string cityName)
        {
            var parameter = new SqlParameter("@cityName", cityName);

            await using var connection = Connection;
            await using var sqlCommand = new SqlCommand("dbo.searchCities", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.Add(parameter);

            await connection.OpenAsync();

            await using var reader = await sqlCommand.ExecuteReaderAsync();
            if (!reader.HasRows)
            {
                return null;
            }

            var result = new List<City>();

            while (await reader.ReadAsync())
            {
                var newRow = new City
                {
                    Id = Convert.ToInt32(reader["CityID"]),
                    CityName = Convert.ToString(reader["CityName"]),
                    StateProvinceId = Convert.ToInt32(reader["StateProvinceID"]),
                    LatestRecordedPopulation = await reader.IsDBNullAsync("LatestRecordedPopulation")
                        ? default(long?)
                        : Convert.ToInt64(reader["LatestRecordedPopulation"]),
                    LastEditedBy = Convert.ToInt32(reader["LastEditedBy"]),
                    ValidFrom = Convert.ToDateTime(reader["ValidFrom"]),
                    ValidTo = Convert.ToDateTime(reader["ValidTo"])
                };

                result.Add(newRow);
            }

            return result;
        }

        #endregion

        #region Generated SQL - Some People Are Okay With This

        // Not something ADO does natively!

        #endregion
    }
}
