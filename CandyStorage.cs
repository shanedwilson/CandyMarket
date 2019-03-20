using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.candyStorage
{
    public class CandyStorage
    {
        public string Owner { get; set; }
        public List<Candy> Candies { get; set; } = new List<Candy>();
        public List<Candy> testCandies = new List<Candy>();

        Candy snickers = new Candy ("Snickers", "Chocolate", "Mars" );
        Candy whatchamacallit = new Candy ("Whatchamacallit", "Chocolate", "Hershey");
        Candy starburst = new Candy("Starburst", "Fruity", "Mars");


        public void addCandy(Candy newCandy)
        {
            Candies.Add(newCandy);
        }

        public void orderCandy()
        {
            Candies.OrderBy(candy => candy.Date);
        }
    }
}
