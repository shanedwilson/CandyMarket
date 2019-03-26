using candy_market.candyStorage;
using candy_market.menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace candy_market
{
   internal class Trade
    {

        internal static void TradeCandy(CandyStorage myStuff, List<CandyStorage> candyOwners)
        {
            List<string> nameList = new List<string>();
            foreach (CandyStorage cstorage in candyOwners)
            {
                nameList.Add(cstorage.Owner);
            }

            var menu = new View();
            menu.AddMenuText("Enter a candy owner's name to trade with.");
            menu.AddMenuText("Press Esc to exit.");
            Console.Write(menu.GetFullMenu());

            var userOption = Console.ReadLine().ToLower();
            Console.WriteLine(userOption);

            if (nameList.Contains(userOption))
            {
                var otherOwner = candyOwners.Where(candies => candies.Owner == userOption).ToList()[0];
                var otherNameArray = otherOwner.Owner.ToCharArray();
                otherNameArray[0] = Char.ToUpper(otherNameArray[0]);
                var otherName = string.Join("", otherNameArray);
                menu.AddMenuText($"Select a candy number from {otherName}'s list below.");
                writeCandies(otherOwner.Candies, menu);
                Console.WriteLine(menu.GetFullMenu());
                var otherOption = Int32.Parse(Console.ReadLine());

                var menu2 = new View();
                menu2.AddMenuText("Select a candy number from your list below.");
                writeCandies(myStuff.Candies, menu2);
                Console.Write(menu2.GetFullMenu());
                var myOption = Int32.Parse(Console.ReadLine());

                Console.WriteLine($"You traded your {myStuff.Candies[myOption - 1].Name} for {otherName}'s {otherOwner.Candies[otherOption - 1].Name}. Press enter to continue.");

                otherOwner.addCandy(myStuff.Candies[myOption - 1]);
                myStuff.addCandy(otherOwner.Candies[otherOption - 1]);
                otherOwner.Candies.RemoveAt(otherOption - 1);
                myStuff.Candies.RemoveAt(myOption - 1);
                myStuff.orderCandy();
                otherOwner.orderCandy();
            }
            else
            {
                menu.AddMenuText("That name was not found. Press enter to continue");
                Console.WriteLine(menu.GetFullMenu());
            }


            Console.ReadLine();
            var exit = false;
            while (!exit)
            {
                var userInput = Menus.MainMenu();
                exit = Menus.TakeActions(myStuff, userInput, candyOwners);
            }
        }

        internal static void writeCandies(List<Candy> candies, View menu)
        {
            foreach (Candy candy in candies)
            {
                menu.AddMenuOption($"{candy.Name}, Flavor: {candy.Flavor}, Manufacturer: {candy.Maker}, Received On: {candy.Date}");
            }
        }
    }
}
