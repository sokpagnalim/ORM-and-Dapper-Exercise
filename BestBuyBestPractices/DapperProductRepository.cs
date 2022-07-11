using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BestBuyBestPractices
{   
   
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
           _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUE (@name,@price,@categoryID);",
               new {Name=name, Price=price,CategoryID=categoryID});
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID=@productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales WHERE ProductID=@productID;",
                new {productID=productID});
            _connection.Execute("DELET FROM reviews WHERE ProductID=@productID;",
                new {productID=productID});
            Console.WriteLine("Product Deleted");
              
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT*FROM Products;");
        }

        public void UpdateProduct(int productID, string updateName)
        {
            _connection.Execute("UPDATE products SET Name=@updateName WHERE ProductID=@productID;",
                new {updatedName=updateName,productID=productID});
            Console.WriteLine("Product Updated");
        }
    }
}
