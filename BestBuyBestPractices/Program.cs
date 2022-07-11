using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace BestBuyBestPractices
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);
            var repo = new DapperDepartmentRepository(conn);
            Console.WriteLine("Type a new department ");
            var newDepartment = Console.ReadLine();
            repo.InsertDepartment(newDepartment);
            var departments = repo.GetAllDepartments();
            foreach(var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

            var prodRepo=new DapperProductRepository(conn);
            var products=prodRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID}:{prod.Name} ${prod.Price}");
            }
            {
                //Console.WriteLine();
                //Console.WriteLine("what is the name of the new Product?");
                //var prodName=Console.ReadLine();
                //Console.WriteLine("what is the Price?");
                //var prodPrice=double.Parse(Console.ReadLine());
                //Console.WriteLine("what is its Category ID?");
                //var prodCategoryID= int.Parse(Console.ReadLine());
                //prodRepo.CreateProduct(prodName, prodPrice, prodCategoryID);
                //products = prodRepo.GetAllProducts();
                //Console.WriteLine();
                //foreach(var prod in products)
                //{
                //    Console.WriteLine($"{prod.ProductID}: {prod.Name} ${prod.Price}");
                //}
                Console.WriteLine("What is the product ID that you want to update?");
                var productID=int.Parse(Console.ReadLine());
                Console.WriteLine("What is the updated name?");
                var newProductName=Console.ReadLine();
                prodRepo.UpdateProduct(productID, newProductName);
                products=prodRepo.GetAllProducts();

                Console.WriteLine();
                foreach(var prod in products)
                {
                   Console.WriteLine($"{prod.ProductID}: {prod.Name} ${prod.Price}");
                }
                Console.WriteLine();

                Console.WriteLine("What is the product that you want to delete?");
                productID=int.Parse(Console.ReadLine());
                prodRepo.DeleteProduct(productID);
                products = prodRepo.GetAllProducts();

                Console.WriteLine();
                foreach(var prod in products)
                {
                    Console.WriteLine($"{prod.ProductID}: {prod.Name} ${prod.Price}");
                }

            }
        }
    }
}
