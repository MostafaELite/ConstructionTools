using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructionTools.Domain.Entities;

namespace ConstructionTools.Services.Abstract
{
    /// <summary>
    /// Contains Logic related to ConstructionTools and act as an orchestrator for the entity
    /// </summary>
    public interface IConstructionToolsService
    {
        /// <summary>
        /// Get All Construction Tools
        /// </summary>
        /// <returns></returns>
        IQueryable<ConstructionTool> GetAll();

        /// <summary>
        /// Calculates the fees of renting a specific tool for a number of days
        /// </summary>
        /// <param name="toolId">the id of the tool to rent</param>
        /// <param name="numberOfRentingDays">the number of renting days</param>
        /// <returns></returns>
        double CalculateFees(int toolId, int numberOfRentingDays);
    }
}
