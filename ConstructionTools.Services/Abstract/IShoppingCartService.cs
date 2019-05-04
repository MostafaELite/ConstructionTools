using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConstructionTools.Domain.Entities;

namespace ConstructionTools.Services.Abstract
{
    /// <summary>
    /// Contains Logic related to ConstructionTools and act as an orchestrator for the entity
    /// </summary>
    public interface IShoppingCartService
    {
      
        /// <summary>
        /// Adds an item to the shopping cart , but since we don't have a shopping cart it will just create a new record in the db
        /// </summary>
        /// <returns>flag indicating whether the operation is successful  </returns>
        bool AddShoppingCartItem(int toolId, short numberOfRentingDays);

        /// <summary>
        /// Returns all items in a certain shopping cart , since we only have one shopping cart , it will return all shopping cart items in the db
        /// </summary>
        /// <returns></returns>
        IEnumerable<ShoppingCartItem> GetShoppingCartItems();

        /// <summary>
        /// Perform the checkout logic like which is not implemented in our case
        /// </summary>
        /// <returns>the string content of the invoice</returns>
        string Checkout();

    }
}
