using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Domain.Interfaces;
using ConstructionTools.Repository.Abstract;
using System.Linq;

namespace ConstructionTools.Services.Concreate.FeesCalculators
{
    //Regular – rental price is one-time rental fee plus premium fee for the first 2 days plus regular fee for the number of days over 2.
    public class RegularToolsFeesCalculator : IFeesCalculator
    {
        private readonly IRepository<Fee> _feesRepo;
        public RegularToolsFeesCalculator(IRepository<Fee> feesRepo)
        {
            _feesRepo = feesRepo;
        }

        public byte RewardingLoyaltyPoint => 1;

        public double Calculate(int numberOfRentingDays)
        {
            //Number of days customer is charged a premium fee for this category of tools
            const int numberOfPremiumDays = 2;

            var requiredFeesForCalculation = _feesRepo.Query();

            var oneTimeRentalFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.OneTime).FeeValue;
            var premiumFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.Premium).FeeValue;
            var regularFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.Regular).FeeValue;

            if (numberOfRentingDays <= numberOfPremiumDays)
                return oneTimeRentalFeeValue + (numberOfRentingDays * premiumFeeValue);

            return oneTimeRentalFeeValue + (numberOfPremiumDays * premiumFeeValue) + ((numberOfRentingDays - numberOfPremiumDays) * regularFeeValue);

        }
    }
}
