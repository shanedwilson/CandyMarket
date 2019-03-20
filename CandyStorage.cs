using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.candyStorage
{
    public class CandyStorage
    {
<<<<<<< HEAD
        public List<Candy> _myCandy { get; set; } = new List<Candy>();
=======
        public string Owner { get; set; }
        public List<Candy> Candies { get; set; } = new List<Candy>();
>>>>>>> master

        public void addCandy(Candy newCandy)
        {
            Candies.Add(newCandy);
        }

        public void orderCandy()
        {
            Candies.OrderBy(candy => candy.Date);
        }

        //public void Eat(index)
        //{
        //        _myCandy.RemoveAt(index);
        //}
    }
}
