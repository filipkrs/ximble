using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ximble.Api.Pagination
{
    public class PagedList<T>
    {
        public List<T> Items { get; private set; }
        public Pager Pager { get; private set; }

        public PagedList(
            int totalItems,
            int page,
            int pageSize,
            List<T> items)
        {
            Items = items;
            Pager = new Pager(totalItems, pageSize, page);
        }
    }
}
