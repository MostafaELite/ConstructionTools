using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Interfaces;
using ConstructionTools.Repository.Abstract;
using ConstructionTools.Services.Abstract;
using ConstructionTools.Services.Concreate.FeesCalculators;
using System.Linq;

namespace ConstructionTools.Services.Concreate
{
    /// <summary>
    /// Contains Logic related to ConstructionTools and act as an orchestrator for the entity
    /// </summary>
    public class ConstructionToolsService : IConstructionToolsService
    {
        private readonly IRepository<ConstructionTool> _toolsRepo;
        private readonly IFeesCalculatorFactory _feesCalculator;

        public ConstructionToolsService(IRepository<ConstructionTool> toolsRepo, IFeesCalculatorFactory feesCalculator)
        {
            _toolsRepo = toolsRepo;
            _feesCalculator = feesCalculator;
        }

        public IQueryable<ConstructionTool> GetAll() => _toolsRepo.Query();
        public double CalculateFees(int toolId, int numberOfRentingDays)
        {
            var selectedTool = _toolsRepo.Query().First(tool => tool.ToolId == toolId);
            var feesCalculator = _feesCalculator.GetFeesCalculator(selectedTool);
            var rentingCost = feesCalculator.Calculate(numberOfRentingDays);
            return rentingCost;

        }
    }


}

