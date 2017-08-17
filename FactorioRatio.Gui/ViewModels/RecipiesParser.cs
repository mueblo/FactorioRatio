using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace FactorioRatio.Gui.ViewModels
{
    public static class RecipiesParser
    {
        public static IList<Recipe> Parse(string filePath)
        {
            var result = new List<Recipe>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var splittedLine = line.Split('@');
                var name = splittedLine[0].Trim();
                var timeStr = splittedLine[1].Trim().Replace(" seconds", string.Empty);
                double time;
                double.TryParse(timeStr, NumberStyles.Any, CultureInfo.InvariantCulture, out time);

                var nbItemsStr = Regex.Match(splittedLine[2], " Produces : ([0-9]*):.*,  ").Groups[1].Value;
                int nbItems = 1;
                int.TryParse(nbItemsStr, NumberStyles.Any, CultureInfo.InvariantCulture, out nbItems);
                var ingredientsStr = Regex.Match(splittedLine[3], " Ingredients: (.*)").Groups[1].Value;
                var resources = new List<ResourceCost>();
                foreach (var ingredientStr in ingredientsStr.Split(','))
                {
                    var trimmedIngredient = ingredientStr.Trim(' ');
                    var match = Regex.Match(trimmedIngredient, "([0-9]*):(.*)");
                    if (match.Success)
                    {
                        var costStr = match.Groups[1].Value;
                        var ingredientName = match.Groups[2].Value;
                        var cost = int.Parse(costStr, CultureInfo.InvariantCulture);
                        var resourceCost = new ResourceCost(cost, ingredientName);
                        resources.Add(resourceCost);
                    }
                }
                var item = new Recipe(name, TimeSpan.FromSeconds(time), nbItems, resources.ToArray());
                result.Add(item);
            }
            return result;
        }
    }
}