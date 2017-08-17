using System;

namespace FactorioRatio.Gui.ViewModels
{
    public class FactoryCostResult
    {
        public FactoryCostResult(Recipe gameItemDef, double numberOfItems, double factoryFactor)
        {
            GameItemDef = gameItemDef;
            ResourceName = gameItemDef.Name;
            NumberOfItems = numberOfItems;  
            NumberOfFactory = numberOfItems * (factoryFactor / gameItemDef.ItemsPerSecond);
        }

        public bool IsNumberOfFactoryRound => Math.Abs(Math.Round(NumberOfFactory) - NumberOfFactory) < 0.001;
        public Recipe GameItemDef { get; }
        public string ResourceName { get; }
        public double NumberOfItems { get; }
        public double NumberOfFactory { get; }
        public double ItemPerMinute => NumberOfFactory * GameItemDef.ItemsPerSecond * 60;
    }
}