using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Interfaces;

namespace ConstructionTools.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        public ShoppingCartItemViewModel()
        {

        }

        public ShoppingCartItemViewModel(ShoppingCartItem model, IFeesCalculatorFactory feesCalculatorFactory)
        {
            ShoppingCartItemId = model.ShoppingCartItemId;
            ToolId = model.ToolId;
            ToolName = model.Tool.ToolName;
            NumberOfRentingDays = model.NumberOfRentingDays;
            model.Tool.FeesCalculatorFactory = feesCalculatorFactory;
            Cost = model.Tool.FeesCalculator.Calculate(model.NumberOfRentingDays);
            LoyaltyPoints = model.Tool.FeesCalculator.RewardingLoyaltyPoint;
            
        }



        public int ShoppingCartItemId { get; set; }
        public int ToolId { get; set; }
        public string ToolName { get; set; }
        public short NumberOfRentingDays { get; set; }
        public double Cost { get; set; }
        public byte LoyaltyPoints { get; set; }
    }
}
