using ConstructionTools.Services.Abstract;
using ConstructionTools.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConstructionTools.Domain.Interfaces;

namespace ConstructionTools.Api.Controllers
{
    [EnableCors("Public")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly IFeesCalculatorFactory _feesCalculatorFactory;

        public ShoppingCartController(IShoppingCartService shoppingCartService, ILogger<ShoppingCartController> logger,IFeesCalculatorFactory feesCalculatorFactory)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
            _feesCalculatorFactory = feesCalculatorFactory;
        }

        //Usually this action should return a response object not just a single value
        [HttpPost("AddToShoppingCart/{toolId}/{numberOfRentingDays}")]
        public ActionResult<bool> Post(int toolId, short numberOfRentingDays) =>
            _shoppingCartService.AddShoppingCartItem(toolId, numberOfRentingDays);

        public ActionResult<IEnumerable<ShoppingCartItemViewModel>> Get() =>
            _shoppingCartService.GetShoppingCartItems().Select(item => new ShoppingCartItemViewModel(item, _feesCalculatorFactory)).ToArray();

        [HttpGet("Checkout")]
        public ActionResult Checkout()
        {
            _logger.LogInformation("Checking Out ... Payment Done");
            var invoiceContent = _shoppingCartService.Checkout();

            //This File managing code should be moved to application layer (if created) 
            var currentDirectory = Directory.GetCurrentDirectory();
            var invoiceFileName = $"Invoice -${Guid.NewGuid()}.txt";
            var invoicesFolder = string.Concat(currentDirectory, "\\Invoices\\");
            if (!Directory.Exists(invoicesFolder))
                Directory.CreateDirectory(invoicesFolder);

            var fullFilePath = invoicesFolder + invoiceFileName;
            System.IO.File.WriteAllText(fullFilePath, invoiceContent);
            var fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
            return File(fileBytes, "application/force-download", invoiceFileName);
        }
    }
}