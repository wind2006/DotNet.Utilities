using System;

namespace Molin_CRM.Model
{
    public class Sell
    {
        public string Name { get; set; }

        public string Customer { get; set; }

        public int Number { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Math.Round(Price * Number, 2);
            }
        }

        public DateTime OptTime
        {
            get;
            set;
        }
    }
}