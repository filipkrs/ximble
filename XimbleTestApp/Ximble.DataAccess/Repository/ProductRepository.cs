using System;
using System.Collections.Generic;
using System.Text;
using Ximble.DataAccess.Model;
using Ximble.Domain.IRepository;

namespace Ximble.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> Search(string name, DateTime SellingStartDate, string keywords, int page, int pagesize)
        {
            using (var ctx = new AdventureWorks2017Context())
            {
                
            }
            throw new NotImplementedException();
        }
    }
}
