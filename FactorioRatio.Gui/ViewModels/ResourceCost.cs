namespace FactorioRatio.Gui.ViewModels
{
    public class ResourceCost
    {
        public ResourceCost(int cost, string itemDefName)
        {
            Cost = cost;
            ItemDefName = itemDefName;
        }

        public int Cost { get; }
        public string ItemDefName { get; }
    }
}