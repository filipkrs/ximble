using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ximble.Api.Pagination;
using Ximble.Domain.IRepository;
using Ximble.Domain.Entities;

namespace Ximble.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }


        [HttpGet("search")]
        public IActionResult Search(string name, DateTime sellingStartDate, string keywords, int page = 1, int pagesize = 10)
        {
            var products = productRepository
                .Search(name, sellingStartDate, keywords, page, pagesize);

            var result = new PagedList<Product>(products.TotalCount, page, pagesize, products.Items);
            return Ok(result);    
        }

        [HttpGet("purchasing-report")]
        public IActionResult PurchasingReport(DateTime from, DateTime to)
        {
            /*
             * select pod.duedate as Date, sum(pod.linetotal) as SumOfTraffic, sum(pod.orderqty) as ProductsSold 
                from purchasing.purchaseorderdetail pod
                where pod.duedate > '2013-01-01'
                and pod.duedate < '2014-01-01'
                group by pod.duedate
                order by pod.duedate

                select sum(pod.linetotal) as TotalTraffic, sum(pod.orderqty) as ProductsSold
                from purchasing.purchaseorderdetail pod
                where pod.duedate > '2013-01-01'
                and pod.duedate < '2014-01-01'
             */
            return null;
        }
    }
}