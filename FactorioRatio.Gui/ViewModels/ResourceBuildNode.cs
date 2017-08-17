using System.Collections.Generic;

namespace FactorioRatio.Gui.ViewModels
{
    public class ResourceBuildNode
    {
        public ResourceBuildNode(FactoryCostResult cost, IList<ResourceBuildNode> nodes)
        {
            Cost = cost;
            Nodes = nodes;
        }

        public FactoryCostResult Cost { get; }
        public IList<ResourceBuildNode> Nodes { get; }
    }
}