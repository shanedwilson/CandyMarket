using System;
using System.Collections.Generic;
using System.Linq;
using candy_market.candyStorage;

namespace candy_market
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = SetupNewApp();

			var exit = false;
			while (!exit)
			{
				var userInput = MainMenu();
				exit = TakeActions(db, userInput);
			}
		}

		internal static CandyStorage SetupNewApp()
		{
			Console.Title = "Cross Confectioneries Incorporated";
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;

            Candy snickers = new Candy("Snickers", "Chocolate", "Mars");
            Candy whatchamacallit = new Candy("Whatchamacallit", "Chocolate", "Hershey");
            Candy starburst = new Candy("Starburst", "Fruity", "Mars");

            var db = new CandyStorage();
            db.Candies.Add(snickers);
            db.Candies.Add(snickers);
            db.Candies.Add(whatchamacallit);
            db.Candies.Add(snickers);
            db.Candies.Add(starburst);
            db.Candies.Add(whatchamacallit);

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

		private static bool TakeActions(CandyStorage db, ConsoleKeyInfo userInput)
		{
			Console.Write(Environment.NewLine);

			if (userInput.Key == ConsoleKey.Escape)
				return true;

			var selection = userInput.KeyChar.ToString();
			switch (selection)
			{
				case "1": AddNewCandy(db);
					break;
				case "2": EatCandyByFlavor(db);
					break;
				default: return true;
			}
			return true;
		}

		internal static void AddNewCandy(CandyStorage db)
		{
            Console.WriteLine("Might you know the name of the candy you wish to add?");
            var candyName = Console.ReadLine().ToString();
            Console.WriteLine("And might you know the manufacturer's name of the candy you wish to add?");
            var candyManufacturer = Console.ReadLine().ToString();
            Console.WriteLine("And might you know the flavor profile of the candy you wish to add?");
            var candyFlavor = Console.ReadLine().ToString();

            var newCandy = new Candy(candyName, candyManufacturer, candyFlavor);

			db.addCandy(newCandy);
			Console.WriteLine($"Now you own the candy {newCandy.Name}");
            Console.ReadKey();
            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput);
            }
        }

		private static void EatCandyByFlavor(CandyStorage db)

		{   var candyList = db.Candies;
            List<string> flavorList = new List<string>();
            List<Candy> candyByFlavor = new List<Candy>();
            var flavorMenu = new View();

            Random random = new Random();
            int randNum = random.Next(0, candyList.Count);

            foreach(var candy in candyList)
            {
                if(flavorList.Contains(candy.Flavor) == false)
                flavorList.Add(candy.Flavor);
            }

            foreach (var flavor in flavorList)
                flavorMenu.AddMenuOption(flavor);

            Console.Write(flavorMenu.GetFullMenu());

            Console.WriteLine();
            Console.WriteLine("Please select the flavor you would like to eat.");
            var chosenFlavorNumber = Int32.Parse(Console.ReadLine());

            var filteredCandy = candyList.Where(c => c.Flavor.Contains(flavorList[chosenFlavorNumber -1])).ToList();
            candyList.Remove(filteredCandy[0]);

            Console.WriteLine(candyList.Count);
            Console.ReadKey();


            //candyList.RemoveAt(randNum);
            //Console.WriteLine($"You have {candyList.Count} pieces of candy left.");
            //Console.ReadKey();
            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput);
            }
        }
    }
}
