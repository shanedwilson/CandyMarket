using System;
using System.Collections.Generic;
using System.Linq;
using candy_market.candyStorage;
using candy_market.eatCandy;
using candy_market.menus;


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
				var userInput = Menus.MainMenu();
				exit = Menus.TakeActions(db, userInput, candyOwners);
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
    }       
}