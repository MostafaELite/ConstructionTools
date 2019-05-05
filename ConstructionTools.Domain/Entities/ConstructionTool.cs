using ConstructionTools.Domain.Enums;
using ConstructionTools.Domain.Interfaces;

namespace ConstructionTools.Domain.Entities
{
    /// <summary>
    /// Since there isn't a lot of business in the project i will just stick to an animec domain model 
    /// </summary>
    public class ConstructionTool
    {
        #region Constructors

        public ConstructionTool()
        {

        }

        #endregion

        //Usually all of these properties should have private setter and this object should be immutable
        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public ToolCategory ToolCategory { get; set; }

        public IFeesCalculator FeesCalculator
        {
            get
            {
                _feesCalculator = _feesCalculator ?? _feesCalculatorFactory?.GetFeesCalculator(this);
                return _feesCalculator;
            }
        }

        
        public IFeesCalculatorFactory FeesCalculatorFactory
        {
            get => _feesCalculatorFactory;
            set => _feesCalculator = value.GetFeesCalculator(this);
        }
        private IFeesCalculator _feesCalculator;
        private IFeesCalculatorFactory _feesCalculatorFactory;
    }
}
