using candy_market.candyStorage;
using candy_market.menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market.remove
{
    internal class Remove
    {
        public static void RemoveCandy(Candy candy, CandyStorage db, List<CandyStorage> candyOwners)
        {
            var theCandy = db.Candies;

            theCandy.Remove(candy);

            Console.WriteLine();
            Console.WriteLine("You have these candies left:");

            foreach (var c in theCandy)
            {
                Console.WriteLine($"{c.Name} acquired {c.Date}.");
            }

            Console.WriteLine();
            Console.WriteLine("Hit enter to continue.");
            Console.ReadKey();

            var exit = false;
            while (!exit)
            {
                var userInput = Menus.MainMenu();
                exit = Menus.TakeActions(db, userInput, candyOwners);
            }


        }
    }
}
