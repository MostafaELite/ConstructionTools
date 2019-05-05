using System;
using ConstructionTools.Domain.Entities;
using ConstructionTools.Domain.Enums;
using ConstructionTools.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace ConstructionTools.DataAccess
{
    public class ConstructionToolsDb : DbContext
    {
        private readonly IServiceProvider _serviceProvider;

        public ConstructionToolsDb(DbContextOptions<ConstructionToolsDb> ctxOptions, IServiceProvider serviceProvider) : base(ctxOptions)
        {
            _serviceProvider = serviceProvider;
        }
        public DbSet<ConstructionTool> ConstructionTools { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EntityConfiguration.ConfigureConstructionTools(modelBuilder);
            EntityConfiguration.ConfigureFees(modelBuilder);
            EntityConfiguration.ConfigureShoppingCartItem(modelBuilder);

            var fees = new[]
            {
                new Fee {FeeId=1,FeeName = "One-Time", FeeValue = 100 ,FeeType =  FeesType.OneTime },
                new Fee {FeeId=2,FeeName = "Premium", FeeValue = 60   ,FeeType =  FeesType.Premium},
                new Fee {FeeId=3,FeeName = "Regular", FeeValue = 40   ,FeeType =  FeesType.Regular }
            };
            modelBuilder.Entity<Fee>().HasData(fees);

            
            var tools = new[]
            {
                new ConstructionTool() {ToolId=1,ToolName = "Caterpillar bulldozer", ToolCategory  = ToolCategory.HeavyTool},
                new ConstructionTool() {ToolId=2,ToolName = "KamAZ truck", ToolCategory = ToolCategory.RegularTool},
                new ConstructionTool() {ToolId=3,ToolName = "Komatsu crane", ToolCategory = ToolCategory.HeavyTool},
                new ConstructionTool() {ToolId=4,ToolName = "Volvo steamroller", ToolCategory = ToolCategory.RegularTool},
                new ConstructionTool() {ToolId=5,ToolName = "Bosch jackhammer", ToolCategory = ToolCategory.SpecializedTool}
            };
            modelBuilder.Entity<ConstructionTool>().HasData(tools);


        }
    }
}
