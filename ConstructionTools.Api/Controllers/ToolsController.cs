using System;
using System.Collections;
using ConstructionTools.Domain.Entities;
using ConstructionTools.Services.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ConstructionTools.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Public")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IConstructionToolsService _toolsService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ToolsController> _logger;

        public ToolsController(IConstructionToolsService toolsService, IMemoryCache memoryCache,ILogger<ToolsController> logger)
        {
            _toolsService = toolsService;
            _cache = memoryCache;
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<ConstructionTool> Get()
        {
            IEnumerable<ConstructionTool> tools;
            if (!_cache.TryGetValue("CachedToolsList", out tools))
            {
                tools = _toolsService.GetAll().ToArray();
                _cache.Set("CachedToolsList", tools);
            }

            return tools;
        }

        [HttpGet("CalculateFees/{toolId}/{numberOfRentingDays}")]
        public ActionResult<double> CalculateFees(int toolId, int numberOfRentingDays)
        {
            try
            {
                _logger.LogInformation("Calculating Fees For Item : " + toolId);
                return _toolsService.CalculateFees(toolId, numberOfRentingDays); 
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
                throw;
            }
         
        }
    }

}
