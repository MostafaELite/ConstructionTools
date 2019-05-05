using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructionTools.Domain.Entities
{
    /// <summary>
    /// an item in a shopping , usually associated with a ShoppingCart entity but for simplicity and because it's one user only the shopping cart entity has been ignored 
    /// </summary>
    public class ShoppingCartItem
    {
        public ShoppingCartItem(int toolId, short numberOfRentingDays)
        {
            ToolId = toolId;
            NumberOfRentingDays = numberOfRentingDays;
    }

        public int ShoppingCartItemId { get; set; }
        public int ToolId { get; set; }
        public short NumberOfRentingDays { get; set; }


        public virtual ConstructionTool Tool{ get; set; }
    }
}
