using System;
using System.Collections.Generic;

namespace candy_market
{
    public class CandyStorage
    {
        public List<Candy> _myCandy { get; set; } = new List<Candy>();

        internal IList<string> GetCandyTypes()
        {
            throw new NotImplementedException();
        }

        internal Candy SaveNewCandy(Candy newCandy)
        {
            throw new NotImplementedException();
        }

        //public void Eat(index)
        //{
        //        _myCandy.RemoveAt(index);
        //}
    }
}
