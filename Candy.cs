using System;

namespace candy_market
{
    internal class Candy
    {
        public string Name { get; set; }
        public string Flavor { get; set; }
        public string Maker { get; set; }

        public Candy(string name, string flavor, string maker)
        {
            Name = name;
            Flavor = flavor;
            Maker = maker;
            DateTime Date = DateTime.Now;
        }
    }
}