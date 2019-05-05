using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Domain.Interfaces;
using ConstructionTools.Repository.Abstract;
using System.Linq;

namespace ConstructionTools.Services.Concreate.FeesCalculators
{
    //Specialized – rental price is premium fee for the first 3 days plus regular fee times the number of days over 3.
    public class SpecializedToolsFeesCalculator : IFeesCalculator
    {
        private readonly IRepository<Fee> _feesRepo;
        public SpecializedToolsFeesCalculator(IRepository<Fee> feesRepo)
        {
            _feesRepo = feesRepo;
        }

        public byte RewardingLoyaltyPoint => 1;

        public double Calculate(int numberOfRentingDays)
        {
            //Number of days customer is charged a premium fee for this category of tools
            const int numberOfPremiumDays = 3;

            var requiredFeesForCalculation = _feesRepo
                .Query(fee => fee.FeeType == FeesType.Premium || fee.FeeType == FeesType.Regular);

            var premiumFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.Premium).FeeValue;
            var regularFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.Regular).FeeValue;

            if (numberOfRentingDays <= numberOfPremiumDays)
                return numberOfRentingDays * premiumFeeValue;

            return (numberOfPremiumDays * premiumFeeValue) + ((numberOfRentingDays - numberOfPremiumDays) * regularFeeValue);
        }
    }
}
