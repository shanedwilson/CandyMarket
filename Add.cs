using candy_market.candyStorage;
using candy_market.menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market
{
    internal class Add
    {
        internal static void AddNewCandy(CandyStorage db, List<CandyStorage> candyOwners)
        {
            var addMenu = new View();
            Console.Write(addMenu.GetFullMenu());
            Console.WriteLine("Might you know the name of the candy you wish to add?");
            var candyName = Console.ReadLine().ToString();
            Console.Write(addMenu.GetFullMenu());
            Console.WriteLine("And might you know the manufacturer's name of the candy you wish to add?");
            var candyManufacturer = Console.ReadLine().ToString();
            Console.Write(addMenu.GetFullMenu());
            Console.WriteLine("And might you know the flavor profile of the candy you wish to add?");
            var candyFlavor = Console.ReadLine().ToString();

            var newCandy = new Candy(candyName, candyFlavor, candyManufacturer);

            db.addCandy(newCandy);
            Console.Write(addMenu.GetFullMenu());
            Console.WriteLine($"Now you own the candy {newCandy.Name}.");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue.");
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
