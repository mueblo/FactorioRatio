using System;
using System.Collections.Generic;

namespace FactorioRatio.Gui.ViewModels
{
    public class Recipe
    {
        public Recipe(string name, TimeSpan constructTime, params ResourceCost[] resourceCosts)
            : this(name, constructTime, 1, resourceCosts)
        {
        }
        
        public Recipe(string name, TimeSpan constructTime, int itemsPerConstruct = 1, params ResourceCost[] resourceCosts)
        {
            Name = name;
            ConstructTime = constructTime;
            ItemsPerConstruct = itemsPerConstruct;
            ResourceCosts = resourceCosts;
        }
        
        public string Name { get; }
        public TimeSpan ConstructTime { get; }
        public int ItemsPerConstruct { get; }
        public IList<ResourceCost> ResourceCosts { get; }
        public double ItemsPerSecond => ItemsPerConstruct / ConstructTime.TotalSeconds;
    }
}