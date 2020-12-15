using System;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ServingDataTheRightWay.Web.Api
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IDiagnosticContext _diagnosticContext;

        public StatusController(IDiagnosticContext diagnosticContext)
        {
            this._diagnosticContext = diagnosticContext;
        }

        [HttpGet]
        public ActionResult<string> GetStatus()
        {
            this._diagnosticContext.Set("trackingId", Guid.NewGuid());
            return "Alive";
        }
    }
}
