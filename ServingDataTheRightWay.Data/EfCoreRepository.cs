using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServingDataTheRightWay.Data.Models;

namespace ServingDataTheRightWay.Data
{
    public class EfCoreRepository
    {
        private readonly WideWorldImporters _dbContext;
        private readonly ILogger<EfCoreRepository> _logger;

        public EfCoreRepository(WideWorldImporters dbContext, ILogger<EfCoreRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        #region Hardcoded SQL - Don't Do This!
        public async Task<City> GetCityById(int cityId)
        {
            var query =
                $"SELECT CityID, CityName, StateProvinceID, LatestRecordedPopulation, LastEditedBy, ValidFrom, ValidTo FROM Application.Cities WHERE CityID = {cityId}";

            return await this._dbContext.Cities.FromSqlRaw(query).SingleOrDefaultAsync();
        }

        #endregion

        #region Stored Procedure - Doing Better

        public async Task<IEnumerable<City>> SearchCities(string cityName)
        {
            var parameter = new SqlParameter("@cityName", cityName);

            return await this._dbContext.Cities.FromSqlRaw("dbo.SearchCities @cityName", parameter).ToListAsync();
        }

        #endregion

        #region Generated SQL - Some People Are Okay With This

        public async Task<IEnumerable<StateProvince>> GetStates()
        {
            return await this._dbContext.StateProvinces.ToListAsync();
        }

        #endregion
    }
}
