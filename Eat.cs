using candy_market.candyStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using candy_market.remove;

namespace candy_market.eatCandy
{
    internal class Eat
    {
        public static void EatCandy(CandyStorage db, List<CandyStorage> candyOwners)

        {
            var theCandy = db.Candies;
            List<string> candyNames = new List<string>();
            var candyMenu = new View();

            foreach (var candy in theCandy)
            {
                if (candyNames.Contains(candy.Name) == false)
                    candyNames.Add(candy.Name);
            }

            foreach (var name in candyNames)
                candyMenu.AddMenuOption(name);

            Console.Write(candyMenu.GetFullMenu());

            Console.WriteLine();
            Console.WriteLine("Please select the candy you would like to eat.");

            var flavorMenuInput = Console.ReadKey();
            var chosenNameNumber = int.Parse(flavorMenuInput.KeyChar.ToString());

            var filteredCandy = theCandy.Where(c => c.Name.Contains(candyNames[chosenNameNumber - 1]))
                .ToList();

            var oldestCandy = filteredCandy.OrderBy(c => c.Date).First();
            Remove.RemoveCandy(oldestCandy, db, candyOwners);
        }
    }
}
