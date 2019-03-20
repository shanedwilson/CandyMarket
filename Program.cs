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

            var shane = new CandyStorage { Owner = "Shane" };
            var marshall = new CandyStorage { Owner = "Marshall" };
            var rich = new CandyStorage { Owner = "Rich" };

            candyOwners.Add(shane);
            candyOwners.Add(marshall);
            candyOwners.Add(rich);

            Candy snickers = new Candy("Snickers", "Chocolate", "Mars");
            Candy whatchamacallit = new Candy("Whatchamacallit", "Chocolate", "Hershey");
            Candy starburst = new Candy("Starburst", "Fruity", "Mars");

            shane.Candies.Add(snickers);
            shane.Candies.Add(snickers);
            shane.Candies.Add(starburst);
            shane.Candies.Add(snickers);
            marshall.Candies.Add(starburst);
            marshall.Candies.Add(whatchamacallit);
            marshall.Candies.Add(whatchamacallit);
            marshall.Candies.Add(snickers);
            rich.Candies.Add(starburst);
            rich.Candies.Add(starburst);
            rich.Candies.Add(starburst);
            rich.Candies.Add(snickers);

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
                    .AddMenuOption("Do you want to trade some candy? Trade it here.")
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
				case "1": AddNewCandy(db, candyOwners);
					break;
				case "2": EatCandy(db);
					break;
                case "3": TradeCandy(db, candyOwners);
                    break;
				default: return true;
			}
			return true;
		}

		internal static void AddNewCandy(CandyStorage db, List<CandyStorage> candyOwners)
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
                exit = TakeActions(db, userInput, candyOwners);
            }
		}

        internal static void TradeCandy(CandyStorage myStuff, List<CandyStorage> candyOwners)
        {
            var menu = new View();
            menu.AddMenuText("Enter a candy owner's name to trade with.");
            menu.AddMenuText("Press Esc to exit.");
            Console.Write(menu.GetFullMenu());

            var userOption = Console.ReadLine().ToLower();
            var otherOwner = candyOwners.Where(candies => candies.Owner.ToLower() == userOption).ToList()[0];
            menu.AddMenuText("Select a candy number from the owner's list below.");
            writeCandies(otherOwner.Candies, menu);
            Console.Write(menu.GetFullMenu());
            var otherOption = Int32.Parse(Console.ReadLine());

            var menu2 = new View();
            menu2.AddMenuText("Select a candy number from your list below.");
            writeCandies(myStuff.Candies, menu2);
            Console.Write(menu2.GetFullMenu());
            var myOption = Int32.Parse(Console.ReadLine());

            Console.WriteLine($"You traded your {myStuff.Candies[myOption - 1].Name} for {otherOwner.Owner}'s {otherOwner.Candies[otherOption - 1].Name}.");

            otherOwner.addCandy(myStuff.Candies[myOption - 1]);
            myStuff.addCandy(otherOwner.Candies[otherOption - 1]);
            otherOwner.Candies.RemoveAt(otherOption - 1);
            myStuff.Candies.RemoveAt(myOption - 1);
            myStuff.orderCandy();
            otherOwner.orderCandy();

            Console.ReadLine();
            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(myStuff, userInput, candyOwners);
            }
        }

        internal static void writeCandies(List<Candy> candies, View menu)
        {
            foreach(Candy candy in candies)
            {
                menu.AddMenuOption($"{candy.Name}, Flavor: {candy.Flavor}, Manufacturer {candy.Maker}, Received on {candy.Date}");
            }
        }

        private static void EatCandy(CandyStorage db)

		{   var candyList = db.Candies;
            Random random = new Random();
            int randNum = random.Next(0, candyList.Count);
            candyList.RemoveAt(randNum);
            Console.WriteLine(candyList.Count);
            Console.ReadKey();
        }
	}       
}

