using System;
using System.Collections.Generic;
using System.Text;
using Ximble.Domain.Entities;

namespace Ximble.Domain.IRepository
{
    public interface IProductRepository
    {
        CountedList<Product> Search(string name, DateTime SellingStartDate, string keywords, int page, int pagesize);
    }
}
