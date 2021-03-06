﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServingDataTheRightWay.Data;
using ServingDataTheRightWay.Data.Models;

namespace ServingDataTheRightWay.Web.Api
{
    [ApiController]
    [Route("[controller]")]
    public class DapperController : ControllerBase
    {
        private readonly DapperRepository _repository;

        public DapperController(DapperRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet("rawSql/byId/{id}")]
        public async Task<ActionResult<City>> GetCityFromHardcodedSqlById(int id)
        {
            return await this._repository.GetCityById(id);
        }

        [HttpGet("storedProc")]
        public async Task<IEnumerable<City>> SearchCitiesStoredProcedure(string cityName)
        {
            return await this._repository.SearchCities(cityName);
        }
    }
}
