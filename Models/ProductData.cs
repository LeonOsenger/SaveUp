using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Models
{
    public class ProductData
    {
        public Guid Id { get; set; }

        public double price { get; set; }

        public string description { get; set; }
    }
}
