using ConstructionTools.Domain.Entities;
using ConstructionTools.Repository.Abstract;
using ConstructionTools.Services.Abstract;
using ConstructionTools.Services.Concreate.FeesCalculators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructionTools.Domain.Interfaces;

namespace ConstructionTools.Services.Concreate
{
    /// <summary>
    /// Contains Logic related to ConstructionTools and act as an orchestrator for the entity
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCartItem> _shoppingCartRepo;
        private readonly IFeesCalculatorFactory _feesCalculatorFactory;
        private readonly IConstructionToolsService _toolsService;

        public ShoppingCartService(IConstructionToolsService toolsService, IRepository<ShoppingCartItem> shoppingCartRepo, IFeesCalculatorFactory feesCalculatorFactory)
        {
            _toolsService = toolsService;
            _shoppingCartRepo = shoppingCartRepo;
            _feesCalculatorFactory = feesCalculatorFactory;
        }

        public bool AddShoppingCartItem(int toolId, short numberOfRentingDays)
        {
            var rentCost = _toolsService.CalculateFees(toolId, numberOfRentingDays);
            var shoppingCartItem = new ShoppingCartItem(toolId, numberOfRentingDays);
            _shoppingCartRepo.Add(shoppingCartItem);
            var result = _shoppingCartRepo.SaveChanges();
            return result;
        }

        public IEnumerable<ShoppingCartItem> GetShoppingCartItems() =>
            _shoppingCartRepo.Query().Include(item => item.Tool);



        public string Checkout()
        {
            //Do some payment logic
            var invoice = GetInvoice();
            ClearCartItems(); return invoice;
        }

        private string GetInvoice()
        {
            var shoppingCartItems = _shoppingCartRepo.Query().Include(item => item.Tool);
            var invoiceTemplate = new StringBuilder(ServicesResources.InvoiceTemplate);
            short loyaltyPoints = 0;
            foreach (var item in shoppingCartItems)
            {
                invoiceTemplate.Append(Environment.NewLine);

                //Get a Tool Fees Calculator and calculate the item cost
                //Should Cache Created Object to avoid duplications
                var itemCalculator = _feesCalculatorFactory.GetFeesCalculator(item.Tool);
                var itemCost = itemCalculator.Calculate(item.NumberOfRentingDays);

                var itemRow = string.Format(ServicesResources.InvoiceRowTemplate, item.Tool.ToolName, item.NumberOfRentingDays, itemCost);
                loyaltyPoints += itemCalculator.RewardingLoyaltyPoint;

                invoiceTemplate.Append(itemRow);
            }
            invoiceTemplate.Append(Environment.NewLine);
            var loyaltyMessage = string.Format(ServicesResources.LoyaltyPointMessage, loyaltyPoints);
            invoiceTemplate.Append(loyaltyMessage);
            return invoiceTemplate.ToString();

        }

        private void ClearCartItems()
        {
            _shoppingCartRepo.Query().ToList().ForEach(item => _shoppingCartRepo.Delete(item));
            _shoppingCartRepo.SaveChanges();
        }


    }


}

