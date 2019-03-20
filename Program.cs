using System;
using candy_market.candyStorage;
using candy_market;
using System.Collections.Generic;
using System.Linq;

namespace candy_market
{
	class Program
	{
		static void Main(string[] args)
		{
            var candyOwners = new List<CandyStorage>();
            var db = SetupNewApp();
            candyOwners.Add(db);

            var exit = false;
			while (!exit)
			{
				var userInput = MainMenu();
				exit = TakeActions(db, userInput, candyOwners);
			}
		}

		internal static CandyStorage SetupNewApp()
		{
			Console.Title = "Cross Confectioneries Incorporated";
			Console.BackgroundColor = ConsoleColor.Magenta;
			Console.ForegroundColor = ConsoleColor.Black;

			var db = new CandyStorage();
			return db;
		}

		internal static ConsoleKeyInfo MainMenu()
		{
			View mainMenu = new View()
					.AddMenuOption("Did you just get some new candy? Add it here.")
					.AddMenuOption("Do you want to eat some candy? Take it here.")
					.AddMenuText("Press Esc to exit.");
			Console.Write(mainMenu.GetFullMenu());
			var userOption = Console.ReadKey();
			return userOption;
		}

		private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput, List<CandyStorage> candyOwners)
		{
			Console.Write(Environment.NewLine);

			if (userInput.Key == ConsoleKey.Escape)
				return true;

			var selection = userInput.KeyChar.ToString();
			switch (selection)
			{
				case "1": AddNewCandy(db);
					break;
				case "2": EatCandy(db);
					break;
                case "3": TradeCandy(db, candyOwners);
                    break;
				default: return true;
			}
			return true;
		}

		internal static void AddNewCandy(CandyStorage db)
		{
            var newCandy = new Candy("Whatchamacallit", "Ass", "Hershey?");
  
			db.addCandy(newCandy);
			Console.WriteLine($"Now you own the candy {newCandy.Name}");
		}

		private static void EatCandy(CandyStorage db)
		{
			throw new NotImplementedException();
		}

        internal static void TradeCandy(CandyStorage db, List<CandyStorage> candyOwners)
        {
            var menu = new View();
            menu.AddMenuOption("Enter a candy owner's name to trade with.");
            menu.AddMenuText("Press Esc to exit.");
            Console.Write(menu.GetFullMenu());
            var userOption = Console.ReadLine();
            var otherOwner = candyOwners.Where(candies => candies.Owner == userOption).ToList();
            writeCandies(otherOwner[0].Candies);
        }

        internal static void writeCandies(List<Candy> candies)
        {
            var counter = 0;
            foreach(Candy candy in candies)
            {
                counter++;
                Console.WriteLine($"{counter}. {candy.Name}, Flavor: {candy.Flavor}, Manufacturer {candy.Maker}, Received on {candy.Date}");
            }
        }
	}
}
