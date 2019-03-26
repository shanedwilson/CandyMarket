using candy_market.candyStorage;
using candy_market.remove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace candy_market
{
    internal class EatRandom
    {
        public static void EatCandyByFlavor(CandyStorage db, List<CandyStorage> candyOwners)
        {
            var theCandy = db.Candies;
            List<string> theFlavors = new List<string>();
            var flavorMenu = new View();

            foreach (var candy in theCandy)
            {
                if (theFlavors.Contains(candy.Flavor) == false)
                    theFlavors.Add(candy.Flavor);
            }

            foreach (var flavor in theFlavors)
                flavorMenu.AddMenuOption(flavor);

            Console.Write(flavorMenu.GetFullMenu());

            Console.WriteLine();
            Console.WriteLine("Please select the flavor you would like to eat.");
            var flavorMenuInput = Console.ReadKey();
            var chosenFlavorNumber = int.Parse(flavorMenuInput.KeyChar.ToString());

            var filteredCandy = theCandy.Where(c => c.Flavor.Contains(theFlavors[chosenFlavorNumber - 1]))
                .ToList();

            Random random = new Random();
            int randNum = random.Next(0, filteredCandy.Count);

            var randomCandyName = filteredCandy[randNum].Name;

            var eatenCandy = filteredCandy.Where(c => c.Name.Contains(randomCandyName))
                .OrderBy(candy => candy.Date)
                .First();

            Console.WriteLine();
            Console.WriteLine($"You ate {eatenCandy.Name} that you acquired {eatenCandy.Date}.");
            Remove.RemoveCandy(eatenCandy, db, candyOwners);
        }
    }
}
