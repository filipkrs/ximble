using System;
using System.Collections.Generic;
using System.Text;

namespace Ximble.Domain.Entities
{
    public class CountedList<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
