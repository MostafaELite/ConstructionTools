using ConstructionTools.Domain.Entities;

namespace ConstructionTools.Domain.Interfaces
{
    /// <summary>
    /// Responsibole for creating FeesCalculator objects
    /// </summary>
    public interface IFeesCalculatorFactory
    {
        /// <summary>
        ///  Creates an object of IFeesCalculator Depending on the ToolTime
        /// </summary>
        /// <param name="tool"></param>
        /// <returns>FeesCalculator OBject</returns>
        IFeesCalculator GetFeesCalculator(ConstructionTool tool);
    }
}
