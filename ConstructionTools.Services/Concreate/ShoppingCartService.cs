using ConstructionTools.Domain.Entities;
using ConstructionTools.Repository.Abstract;
using ConstructionTools.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructionTools.Services.Concreate
{
    /// <summary>
    /// Contains Logic related to ConstructionTools and act as an orchestrator for the entity
    /// </summary>
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCartItem> _shoppingCartRepo;
        private readonly IConstructionToolsService _toolsService;

        public ShoppingCartService(IConstructionToolsService toolsService, IRepository<ShoppingCartItem> shoppingCartRepo)
        {
            _toolsService = toolsService;
            _shoppingCartRepo = shoppingCartRepo;
        }

        public bool AddShoppingCartItem(int toolId, short numberOfRentingDays)
        {
            var rentCost = _toolsService.CalculateFees(toolId, numberOfRentingDays);
            var shoppingCartItem = new ShoppingCartItem(toolId, numberOfRentingDays, rentCost);
            _shoppingCartRepo.Add(shoppingCartItem);
            var result = _shoppingCartRepo.SaveChanges();
            return result;
        }

        public IEnumerable<ShoppingCartItem> GetShoppingCartItems() => _shoppingCartRepo.Query().Include(item => item.Tool);


        public string Checkout()
        {
            //Do some payment logic
            var invoice = GetInvoice();
            ClearCartItems();return invoice;
        }

        private string GetInvoice()
        {
            var shoppingCartItems = _shoppingCartRepo.Query().Include(item => item.Tool);
            var invoiceTemplate = new StringBuilder(ServicesResources.InvoiceTemplate);
            foreach (var item in shoppingCartItems)
            {
                invoiceTemplate.Append(Environment.NewLine);

                var itemRow = string.Format(ServicesResources.RowTemplate,
                    item.Tool.ToolName, item.NumberOfRentingDays, item.Cost);

                invoiceTemplate.Append(itemRow);
            }
            return invoiceTemplate.ToString();

        }

        private void ClearCartItems()
        {
            _shoppingCartRepo.Query().ToList().ForEach(item => _shoppingCartRepo.Delete(item));
            _shoppingCartRepo.SaveChanges();
        }


    }


}

