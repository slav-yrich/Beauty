using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_v1_1
{
    public class Service
    {
        public DateTime dateAndTime;
        public string master;
        public string name;
        public string shortName;
        public decimal price;
        public int discount;
        public int factor;

        public Service(DateTime dateAndTime, string master, string name, string shortName, decimal price, int discount = 0, int factor = 1)
        {
            this.dateAndTime = dateAndTime;
            this.master = master;
            this.name = name;
            this.shortName = shortName;
            this.price = price;
            this.discount = discount;
            this.factor = factor;
        }

        public decimal GetTotalPrice()
        {
            return (price - discount * price / 100) * factor;
        }
    }
}
