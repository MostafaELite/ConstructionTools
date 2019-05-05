using System;
using System.Collections.Generic;
using System.Text;
using ConstructionTools.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstructionTools.DataAccess
{
    public class EntityConfiguration
    {
        public static void ConfigureConstructionTools(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConstructionTool>()
                .HasKey(tool => tool.ToolId);

            modelBuilder.Entity<ConstructionTool>()
                .Property(tool => tool.ToolId)
                .UseSqlServerIdentityColumn();


            modelBuilder.Entity<ConstructionTool>()
                .Property(tool => tool.ToolId).ValueGeneratedOnAdd();


            modelBuilder.Entity<ConstructionTool>()
                .Ignore(tool => tool.FeesCalculator);

            modelBuilder.Entity<ConstructionTool>()
                .Ignore(tool => tool.FeesCalculatorFactory);

            modelBuilder.Entity<ConstructionTool>()
                .Property(tool => tool.ToolName)
                .IsRequired();
        }
        public static void ConfigureFees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fee>()
                .HasKey(fee => fee.FeeId);

            modelBuilder.Entity<Fee>()
                .Property(fee => fee.FeeName)
                .IsRequired();

            modelBuilder.Entity<Fee>()
                .Property(fee => fee.FeeValue)
                .IsRequired();

            
        }
        public static void ConfigureShoppingCartItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartItem>()
                .HasKey(shoppingCartItem => shoppingCartItem.ShoppingCartItemId);

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(shoppingCartItem => shoppingCartItem.NumberOfRentingDays)
                .IsRequired();
          

            modelBuilder.Entity<ShoppingCartItem>()
                .Property(shoppingCartItem => shoppingCartItem.ToolId)
                .IsRequired();

         




        }

    }
}
