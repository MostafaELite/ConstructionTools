using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Domain.Interfaces;
using ConstructionTools.Repository.Abstract;
using System.Linq;

namespace ConstructionTools.Services.Concreate.FeesCalculators
{
    //Heavy – rental price is one-time rental fee plus premium fee for each day rented
    public class HeavyToolsFeesCalculator : IFeesCalculator
    {
        private readonly IRepository<Fee> _feesRepo;
        public HeavyToolsFeesCalculator(IRepository<Fee> feesRepo)
        {
            _feesRepo = feesRepo;
        }

        public byte RewardingLoyaltyPoint => 2;

        public double Calculate(int numberOfRentingDays)
        {
            var requiredFeesForCalculation = _feesRepo.Query();

            var oneTimeRentalFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.OneTime).FeeValue;
            var premiumFeeValue = requiredFeesForCalculation.First(fee => fee.FeeType == FeesType.Premium).FeeValue;

            return oneTimeRentalFeeValue + (numberOfRentingDays * premiumFeeValue);
        }
    }
}
