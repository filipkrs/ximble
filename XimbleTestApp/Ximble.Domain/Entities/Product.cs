using System;
using System.Collections.Generic;
using System.Text;

namespace Ximble.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public DateTime SellStartDate { get; set; }
        public string Description { get; set; }

    }
}
