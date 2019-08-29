using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ximble.Domain.Entities;
using Ximble.Domain.IRepository;

namespace Ximble.DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            this.connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DefaultConnection");
        }

        public CountedList<Product> Search(string name, DateTime sellingStartDate, string keywords, int page, int pagesize)

        {
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "select p.productid, p.name, p.productnumber, p.sellstartdate, pd.description " +
                                     "from production.product p " +
                                     "left join production.productmodel pm on p.productmodelid = pm.productmodelid " +
                                     "left join production.productmodelproductdescriptionculture pmpdc on pm.productmodelid = pmpdc.productmodelid " +
                                     "left join production.productdescription pd on pmpdc.productdescriptionid = pd.productdescriptionid " +
                                     "where p.sellstartdate > @StartDate " +
                                     "and p.name like @Name " +
                                     "and pd.description like @Description " +
                                     "order by p.sellstartdate " +
                                     "offset @Skip rows fetch next @Take rows only; " +
                                     "Select Count(*) " +
                                     "from production.product p " +
                                     "left join production.productmodel pm on p.productmodelid = pm.productmodelid " +
                                     "left join production.productmodelproductdescriptionculture pmpdc on pm.productmodelid = pmpdc.productmodelid " +
                                     "left join production.productdescription pd on pmpdc.productdescriptionid = pd.productdescriptionid " +
                                     "where p.sellstartdate > @StartDate " +
                                     "and p.name like @Name " +
                                     "and pd.description like @Description";

                int skip = (page - 1) * pagesize;

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", "%" + name + "%");
                command.Parameters.AddWithValue("@Description", "%" + keywords + "%");
                command.Parameters.AddWithValue("@Skip", skip);
                command.Parameters.AddWithValue("@Take", pagesize);

                SqlParameter startDate = new SqlParameter("@StartDate", SqlDbType.DateTime);
                startDate.Value = sellingStartDate;
                command.Parameters.Add(startDate);

                int totalCount = 0;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var product = ReadProduct(reader);
                        products.Add(product);
                    }
                    reader.NextResult();

                    while (reader.Read())
                        totalCount = (int)reader[0];

                }
                catch (Exception ex)
                {
                }

                var result = new CountedList<Product>
                {
                    TotalCount = totalCount,
                    Items = products
                };

                return result;
            }
        }

        private Product ReadProduct(SqlDataReader reader)
        {
            var product = new Product
            {
                Id = (int)reader["productid"],
                Name = reader["name"].ToString(),
                ProductNumber = reader["productnumber"].ToString(),
                SellStartDate = Convert.ToDateTime(reader["sellstartdate"]),
                Description = reader["description"].ToString()
            };

            return product;
        }
        
    }
}