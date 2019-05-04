using System;
using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Repository.Abstract;
using ConstructionTools.Services.Concreate.FeesCalculators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Linq.Expressions;
using Moq;

namespace ConstructionTools.Tests
{
    [TestClass]
    public class CalculatorsTests
    {
        [TestMethod]
        //Heavy – rental price is one-time rental fee plus premium fee for each day rented.
        public void HeavyToolsFeeCalculatorTest_ShouldPass()
        {
            var mockedRepo = new Moq.Mock<IRepository<Fee>>();
            var fees = new[]
            {
                new Fee {FeeId=1,FeeName = "One-Time", FeeValue = 100 ,FeeType =  FeesType.OneTime },
                new Fee {FeeId=2,FeeName = "Premium", FeeValue = 60   ,FeeType =  FeesType.Premium},
                new Fee {FeeId=3,FeeName = "Regular", FeeValue = 40   ,FeeType =  FeesType.Regular }
            };

            mockedRepo.Setup(repo => repo.Query()).Returns(new EnumerableQuery<Fee>(fees));

            var heavyToolsFeeCalculator = new HeavyToolsFeesCalculator(mockedRepo.Object);
            const short numberOfRentingdays = 5;
            var actualResult = heavyToolsFeeCalculator.Calculate(numberOfRentingdays);
            const int expected = 400;

            Assert.AreEqual(expected, actualResult);
        }


        [TestMethod]
        //Specialized – rental price is premium fee for the first 3 days plus regular fee times the number of days over 3.
        public void SpecializedToolsFeeCalculatorTest_ShouldPass()
        {
            var mockedRepo = new Moq.Mock<IRepository<Fee>>();
            var fees = new[]
            {
                new Fee {FeeId=1,FeeName = "One-Time", FeeValue = 100 ,FeeType =  FeesType.OneTime },
                new Fee {FeeId=2,FeeName = "Premium", FeeValue = 60   ,FeeType =  FeesType.Premium},
                new Fee {FeeId=3,FeeName = "Regular", FeeValue = 40   ,FeeType =  FeesType.Regular }
            };

            mockedRepo.Setup(repo => repo.Query(It.IsAny<Expression<Func<Fee,bool>>>())).Returns(new EnumerableQuery<Fee>(fees));

            var specializedToolsFeeCalculator = new SpecializedToolsFeesCalculator(mockedRepo.Object);
            const short numberOfRentingdays = 7;
            var actualResult = specializedToolsFeeCalculator.Calculate(numberOfRentingdays);
            const int expected = 340;
            Assert.AreEqual(expected, actualResult);

        }
 

        [TestMethod]
        //Regular – rental price is one-time rental fee plus premium fee for the first 2 days plus regular fee for the number of days over 2.
        public void RegularToolsFeeCalculatorTest_ShouldPass()
        {
            var mockedRepo = new Moq.Mock<IRepository<Fee>>();
            var fees = new[]
            {
                new Fee {FeeId=1,FeeName = "One-Time", FeeValue = 100 ,FeeType =  FeesType.OneTime },
                new Fee {FeeId=2,FeeName = "Premium", FeeValue = 60   ,FeeType =  FeesType.Premium},
                new Fee {FeeId=3,FeeName = "Regular", FeeValue = 40   ,FeeType =  FeesType.Regular }
            };

            mockedRepo.Setup(repo => repo.Query()).Returns(new EnumerableQuery<Fee>(fees));

            var regularToolsFeeCalculator = new RegularToolsFeesCalculator(mockedRepo.Object);
            const short numberOfRentingdays = 9;
            var actualResult = regularToolsFeeCalculator.Calculate(numberOfRentingdays);
            const int expected = 500;
            Assert.AreEqual(expected,actualResult );

        }
    }
}
