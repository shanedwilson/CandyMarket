using candy_market.candyStorage;
using candy_market.eatCandy;
using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market.menus
{
   internal class Menus
    {
        public static ConsoleKeyInfo MainMenu()
        {
            View mainMenu = new View()
                    .AddMenuOption("Did you just get some new candy? Add it here.")
                    .AddMenuOption("Do you want to choose some candy to eat by flavor? Take it here.")
                    .AddMenuOption("Do you want to eat some candy from your collection? Take it here.")
                    .AddMenuOption("Do you want to trade some candy? Trade it here.")
                    .AddMenuText("Press Esc to exit.");

            Console.Write(mainMenu.GetFullMenu());
            var userOption = Console.ReadKey();
            return userOption;
        }

        public static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, List<CandyStorage> candyOwners)
        {
            Console.Write(Environment.NewLine);

            if (userInput.Key == ConsoleKey.Escape)
                return true;

            var selection = userInput.KeyChar.ToString();
            switch (selection)
            {
                case "1":
                    Add.AddNewCandy(db, candyOwners);
                    break;
                case "2":
                    EatRandom.EatCandyByFlavor(db, candyOwners);
                    break;
                case "3":
                    Eat.EatCandy(db, candyOwners);
                    break;
                case "4":
                    Trade.TradeCandy(db, candyOwners);
                    break;
                default: return true;
            }
            return true;
        }
    }
}
