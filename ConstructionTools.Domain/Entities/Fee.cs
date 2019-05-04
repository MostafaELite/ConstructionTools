using ConstructionTools.Domain.Enums;

namespace ConstructionTools.Domain.Entities
{
    public class Fee
    {
        public short FeeId { get; set; }
        public string FeeName { get; set; }
        public double FeeValue { get; set; }
        public FeesType FeeType { get; set; }
    }
}
