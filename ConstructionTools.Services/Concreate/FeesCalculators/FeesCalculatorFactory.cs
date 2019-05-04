using System;
using System.Collections.Generic;
using System.Text;
using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionTools.Services.Concreate.FeesCalculators
{
    public class FeesCalculatorFactory
    {
        private readonly IServiceProvider _services;

        public FeesCalculatorFactory(IServiceProvider services) => _services = services;

        public IFeesCalculator GetFeesCalculator(ConstructionTool tool)
        {
            var feesCalculators = new Dictionary<ToolCategory, IFeesCalculator>
            {
                { ToolCategory.HeavyTool ,  _services.GetService<HeavyToolsFeesCalculator>()},
                { ToolCategory.RegularTool ,  _services.GetService<RegularToolsFeesCalculator>()},
                { ToolCategory.SpecializedTool , _services.GetService<SpecializedToolsFeesCalculator>()}
            };

            return feesCalculators[tool.ToolCategory];
        }
    }
}
