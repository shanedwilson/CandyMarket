using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.candyStorage
{
    public class CandyStorage
    {
        public string Owner { get; set; }
        public List<Candy> Candies { get; set; } = new List<Candy>();

        public CandyStorage()
        {

        }

        public CandyStorage(string name)
        {
            Owner = name.ToLower();
        }

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
