namespace ConstructionTools.Domain.Interfaces
{
    /// <summary>
    /// Contains everything needed to calculate the renting fees for a specific category , could be an abstract class having the fees retrival logic i it
    /// </summary>
    public interface IFeesCalculator
    {
        byte RewardingLoyaltyPoint { get; }
        /// <summary>
        /// Calculates the renting fees for a specific category
        /// </summary>
        /// <param name="numberOfRentingDays">number of days customer want to rent this tool</param>
        /// <returns>cost of renting</returns>
        double Calculate(int numberOfRentingDays);
    }
}
