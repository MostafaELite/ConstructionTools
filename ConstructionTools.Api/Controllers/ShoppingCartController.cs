using ConstructionTools.Domain.Entities;
using ConstructionTools.Services.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConstructionTools.Api.Controllers
{
    [EnableCors("Public")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;

        public ShoppingCartController(IShoppingCartService shoppingCartService,ILogger<ShoppingCartController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        //Usually this action should return a response object not just a single value
        [HttpPost("AddToShoppingCart/{toolId}/{numberOfRentingDays}")]
        public ActionResult<bool> Post(int toolId, short numberOfRentingDays) =>
            _shoppingCartService.AddShoppingCartItem(toolId, numberOfRentingDays);

        public ActionResult<IEnumerable<ShoppingCartItem>> Get() =>
            _shoppingCartService.GetShoppingCartItems().ToArray();

        [HttpGet("Checkout")]
        public ActionResult Checkout()
        {
            _logger.LogInformation("Checking Out ... Payment Done");
            var invoiceContent = _shoppingCartService.Checkout();

            var currentDirectory = Directory.GetCurrentDirectory();
            var invoiceFileName = $"Invoice -${Guid.NewGuid()}.txt";
            var fullFilePath = string.Concat(currentDirectory, "\\Invoices\\", invoiceFileName);

            System.IO.File.WriteAllText(fullFilePath, invoiceContent);
            var fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
            return File(fileBytes, "application/force-download", invoiceFileName);
        }
    }
}