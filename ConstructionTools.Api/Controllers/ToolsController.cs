using System;
using System.Collections;
using ConstructionTools.Domain.Entities;
using ConstructionTools.Services.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionTools.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("Public")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IConstructionToolsService _toolsService;
        private readonly IMemoryCache _cache;

        public ToolsController(IConstructionToolsService toolsService, IMemoryCache memoryCache)
        {
            _toolsService = toolsService;
            _cache = memoryCache;
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
            return _toolsService.CalculateFees(toolId, numberOfRentingDays);

        }
    }

}
