using System;
using System.Collections.Generic;

namespace Advan02
{
    #region Product Model
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
    #endregion

    internal class Program
    {
        #region Task 1 Smart Product Search
        // SearchProducts uses Func delegate because it takes a product and returns a boolean condition
        static List<Product> SearchProducts(List<Product> products, Func<Product, bool> condition)
        {
            List<Product> result = new List<Product>();
            foreach (Product p in products)
            {
                if (condition(p))
                {
                    result.Add(p);
                }
            }
            return result;
        }
        #endregion

        #region Task 3 Custom Report Generator Methods
        // PrintReport uses Action delegate because it performs an operation and returns void
        static void PrintReport(List<Product> products, Action<Product> action)
        {
            foreach (Product p in products)
            {
                action(p);
            }
        }

        // TransformProducts uses Func delegate because it transforms each product into a generic result type TResult
        static List<TResult> TransformProducts<TResult>(List<Product> products, Func<Product, TResult> transformer)
        {
            List<TResult> result = new List<TResult>();
            foreach (Product p in products)
            {
                result.Add(transformer(p));
            }
            return result;
        }

        // FilterProducts uses Predicate delegate which is a specialized Func returning bool
        static List<Product> FilterProducts(List<Product> products, Predicate<Product> predicate)
        {
            List<Product> result = new List<Product>();
            foreach (Product p in products)
            {
                if (predicate(p))
                {
                    result.Add(p);
                }
            }
            return result;
        }
        #endregion

        static void Main(string[] args)
        {
            // Shared Product Catalog
            List<Product> catalog = new List<Product>
            {
                new Product { Id=1, Name="Laptop", Category="Electronics", Price=1200, Stock=10 },
                new Product { Id=2, Name="Phone", Category="Electronics", Price=800, Stock=25 },
                new Product { Id=3, Name="T-Shirt", Category="Clothing", Price=30, Stock=100 },
                new Product { Id=4, Name="Jeans", Category="Clothing", Price=60, Stock=50 },
                new Product { Id=5, Name="Chocolate", Category="Food", Price=5, Stock=200 },
                new Product { Id=6, Name="Coffee Beans", Category="Food", Price=15, Stock=80 },
                new Product { Id=7, Name="C# Book", Category="Books", Price=45, Stock=30 },
                new Product { Id=8, Name="Novel", Category="Books", Price=20, Stock=60 },
                new Product { Id=9, Name="Headphones", Category="Electronics", Price=150, Stock=40 },
                new Product { Id=10, Name="Jacket", Category="Clothing", Price=120, Stock=15 }
            };

            #region Task 1 Execution Smart Product Search
            // 1 All Electronics products
            Console.WriteLine("--- Electronics ---");
            List<Product> electronics = SearchProducts(catalog, p => p.Category == "Electronics");
            foreach (Product p in electronics)
            {
                Console.WriteLine(p.Name + " - $" + p.Price + " (Stock: " + p.Stock + ")");
            }

            // 2 Products cheaper than 50
            Console.WriteLine("\n--- Under $50 ---");
            List<Product> cheapProducts = SearchProducts(catalog, p => p.Price < 50);
            foreach (Product p in cheapProducts)
            {
                Console.WriteLine(p.Name + " - $" + p.Price + " (Stock: " + p.Stock + ")");
            }

            // 3 Products that are in stock stock greater than 0
            Console.WriteLine("\n--- In Stock ---");
            List<Product> inStockProducts = SearchProducts(catalog, p => p.Stock > 0);
            foreach (Product p in inStockProducts)
            {
                Console.WriteLine(p.Name + " - $" + p.Price + " (Stock: " + p.Stock + ")");
            }

            // 4 Clothing products under 100
            Console.WriteLine("\n--- Clothing Under $100 ---");
            List<Product> cheapClothing = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
            foreach (Product p in cheapClothing)
            {
                Console.WriteLine(p.Name + " - $" + p.Price + " (Stock: " + p.Stock + ")");
            }
            #endregion

            #region Task 3 Execution Custom Report Generator
            // 3 1 Print Reports using Action delegate
            Console.WriteLine("\n--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine(p.Name + " - $" + p.Price));

            Console.WriteLine("\n--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine("[" + p.Category + "] " + p.Name + " | Price: $" + p.Price + " | Stock: " + p.Stock));

            // 3 2 Transform Products using Func delegate
            Console.WriteLine("\n--- Summary List ---");
            List<string> summaries = TransformProducts(catalog, p => p.Name + " ($" + p.Price + ")");
            foreach (string s in summaries)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("\n--- Price Labels ---");
            List<string> priceLabels = TransformProducts(catalog, p => p.Name + ": " + (p.Price > 100 ? "Expensive!" : "Affordable"));
            foreach (string label in priceLabels)
            {
                Console.WriteLine(label);
            }

            // 3 3 Filter Products using Predicate delegate
            Console.WriteLine("\n--- Low-Stock Alert ---");
            List<Product> lowStockItems = FilterProducts(catalog, p => p.Stock < 20);
            foreach (Product p in lowStockItems)
            {
                Console.WriteLine("[LOW STOCK] " + p.Name + ": only " + p.Stock + " left!");
            }
            #endregion
        }
    }
}