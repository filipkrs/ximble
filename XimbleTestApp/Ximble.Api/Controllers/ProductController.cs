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
                .Search(name, sellingStartDate, keywords, page, pagesize)
                .ToList();

            var result = new PagedList<Product>(1, page, pagesize, products);
            return Ok(result);    
        }
    }
}