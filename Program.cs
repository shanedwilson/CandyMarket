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
            var candyOwners = new List<CandyStorage>();

            var shane = new CandyStorage("Shane");
            var marshall = new CandyStorage("Marshall");
            var rich = new CandyStorage("Rich");

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
            
            Candy snickers = new Candy("Snickers", "Chocolate", "Mars", new DateTime(2010, 8, 18, 16, 22,0));
            Candy snickers2 = new Candy("Snickers", "Chocolate", "Mars");
            Candy whatchamacallit = new Candy("Whatchamacallit", "Chocolate", "Hershey");
            Candy starburst = new Candy("Starburst", "Fruity", "Mars");

            var db = new CandyStorage();
            db.Candies.Add(snickers);
            db.Candies.Add(snickers2);
            db.Candies.Add(whatchamacallit);
            db.Candies.Add(starburst);

            return db;
		}

		internal static ConsoleKeyInfo MainMenu()
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
				case "2": EatCandyByFlavor(db, candyOwners);
					break;
                case "3": EatCandy(db, candyOwners);
                    break;
                case "4": TradeCandy(db, candyOwners);
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

            var newCandy = new Candy(candyName, candyFlavor, candyManufacturer );

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
            List<string> nameList = new List<string>();
            foreach(CandyStorage  cstorage in candyOwners)
            {
                nameList.Add(cstorage.Owner);
            }

            var menu = new View();
            menu.AddMenuText("Enter a candy owner's name to trade with.");
            menu.AddMenuText("Press Esc to exit.");
            Console.Write(menu.GetFullMenu());

            var userOption = Console.ReadLine().ToLower();
            Console.WriteLine(userOption);
            
            if (nameList.Contains(userOption)) {
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
            } else
            {
                menu.AddMenuText("That name was not found. Press enter to continue");
                Console.WriteLine(menu.GetFullMenu());
            }
            

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
                menu.AddMenuOption($"{candy.Name}, Flavor: {candy.Flavor}, Manufacturer: {candy.Maker}, Received On: {candy.Date}");
            }
        }

		private static void EatCandyByFlavor(CandyStorage db, List<CandyStorage> candyOwners)
		{   var theCandy = db.Candies;
            List<string> theFlavors = new List<string>();
            var flavorMenu = new View();

            foreach (var candy in theCandy)
            {
                if(theFlavors.Contains(candy.Flavor) == false)
                    theFlavors.Add(candy.Flavor);
            }

            foreach (var flavor in theFlavors)
                flavorMenu.AddMenuOption(flavor);

            Console.Write(flavorMenu.GetFullMenu());

            Console.WriteLine();
            Console.WriteLine("Please select the flavor you would like to eat.");
            var chosenFlavorNumber = Int32.Parse(Console.ReadLine());

            var filteredCandy = theCandy.Where(c => c.Flavor.Contains(theFlavors[chosenFlavorNumber - 1]))
                .ToList();

            Random random = new Random();
            int randNum = random.Next(0, filteredCandy.Count);

            var randomCandyName = filteredCandy[randNum].Name;

            var eatenCandy = filteredCandy.Where(c => c.Name.Contains(randomCandyName))
                .OrderBy(candy => candy.Date)
                .First();

            Console.WriteLine($"You ate {eatenCandy.Name} that you acquired {eatenCandy.Date}.");
            theCandy.Remove(eatenCandy);

            Console.WriteLine();
            Console.WriteLine("You have these candies left:");
            foreach(var candy in theCandy)
            {
                Console.WriteLine($"{candy.Name} acquired {candy.Date}.");
            }

            Console.WriteLine();
            Console.WriteLine("Hit enter to continue.");
            Console.ReadKey();

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput, candyOwners);
            }
        }

        private static void EatCandy(CandyStorage db, List<CandyStorage> candyOwners)

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
            var chosenNameNumber = Int32.Parse(Console.ReadLine());

            var filteredCandy = theCandy.Where(c => c.Name.Contains(candyNames[chosenNameNumber - 1]))
                .ToList();

            var oldestCandy = filteredCandy.OrderBy(c => c.Date).First();
            theCandy.Remove(oldestCandy);

            Console.WriteLine("You have these candies left:");
            foreach (var candy in theCandy)
            {
                Console.WriteLine($"{candy.Name} acquired {candy.Date}.");
            }

            Console.WriteLine();
            Console.WriteLine("Hit enter to continue.");
            Console.ReadKey();

            var exit = false;
            while (!exit)
            {
                var userInput = MainMenu();
                exit = TakeActions(db, userInput, candyOwners);
            }
        }
	}       
}