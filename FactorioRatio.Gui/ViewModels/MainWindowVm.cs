using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ReactiveUI;

namespace FactorioRatio.Gui.ViewModels
{
    public class MainWindowVm : ViewModelBase
    {
        private Recipe _selectedItem;
        private Dictionary<string, Recipe> _itemsDic;
        private List<FactoryCostResult> _factories;
        private int _factor;
        private ResourceBuildNode _root;

        public MainWindowVm()   
        {
            var gameItemDefs = RecipiesParser.Parse(@".\Recipies\recipies.txt").ToList();
            ItemDefs = gameItemDefs;

            _itemsDic = ItemDefs.ToDictionary(x => x.Name, x => x);
            Factor = 1;
        }

        public List<Recipe> ItemDefs { get; }

        public Recipe SelectedItem
        {
            get => _selectedItem;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedItem, value);
                ComputeTree();
            }
        }

        public List<FactoryCostResult> Factories
        {
            get { return _factories; }
            set { this.RaiseAndSetIfChanged(ref _factories, value); }
        }

        public ResourceBuildNode Root
        {
            get { return _root; }
            set { this.RaiseAndSetIfChanged(ref _root, value); }
        }

        public int Factor
        {
            get { return _factor; }
            set
            {
                this.RaiseAndSetIfChanged(ref _factor, value);
                ComputeTree();
            }
        }

        private void ComputeTree()
        {
            if (SelectedItem == null)
            {
                return;
            }
            
            var totalCounts = new ConcurrentDictionary<string, double>();
            var firstCount = 1;
            var node = InnerComputeTree(SelectedItem, firstCount, SelectedItem.ItemsPerSecond * Factor, totalCounts);
            Root = new ResourceBuildNode(null, new List<ResourceBuildNode> { node });
            Factories = totalCounts.Select((kvp) => new FactoryCostResult(_itemsDic[kvp.Key], kvp.Value, SelectedItem.ItemsPerSecond * Factor)).ToList();
        }

        private ResourceBuildNode InnerComputeTree(Recipe item, double numberOfItems, double factoryFactor, ConcurrentDictionary<string, double> totalCounts)
        {
            var nodes = new List<ResourceBuildNode>();
            foreach (var resourceCost in item.ResourceCosts)
            {
                var cost = resourceCost.Cost;
                Recipe newItemDef;
                if (!_itemsDic.TryGetValue(resourceCost.ItemDefName, out newItemDef))
                {
                    continue;
                }
                var nbOfItems = cost * numberOfItems;
                var newNode = InnerComputeTree(newItemDef, nbOfItems, factoryFactor, totalCounts);
                nodes.Add(newNode);
            }
            totalCounts.AddOrUpdate(item.Name, numberOfItems, (key, previous) => previous + numberOfItems);
            return new ResourceBuildNode(new FactoryCostResult(item, numberOfItems, factoryFactor), nodes);
        }
    }
}