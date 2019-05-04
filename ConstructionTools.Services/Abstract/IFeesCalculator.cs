using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionTools.Services.Abstract
{
    /// <summary>
    /// Contains everything need to calculate the renting fees for a specific category , could be an abstract class having the fees retrival logic i it
    /// </summary>
    public interface IFeesCalculator
    {
        /// <summary>
        /// Calculates the renting fees for a specific category
        /// </summary>
        /// <param name="numberOfRentingDays">number of days customer want to rent this tool</param>
        /// <returns>cost of renting</returns>
        double Calculate(int numberOfRentingDays);
    }
}
