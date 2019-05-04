using ConstructionTools.Domain.Enums;

namespace ConstructionTools.Domain.Entities
{
    /// <summary>
    /// Since there isn't a lot of business in the project i will just stick to an animec domain model 
    /// </summary>
    public class ConstructionTool
    {
        //Usually all of these properties should have private setter and this object should be immutable
        public int ToolId{ get; set; }
        public string ToolName { get; set; }
        public ToolCategory ToolCategory { get; set; }

    }
}
