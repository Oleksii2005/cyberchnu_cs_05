using System;
using System.Collections.Generic;

namespace Store
{
    public interface IProduct
    {
        string Name { get; set; }
        decimal Price { get; set; }
        decimal CalculateDiscount();
    }

    public abstract class Product : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public abstract decimal CalculateDiscount();
    }

    public class Book : Product
    {
        public int Pages { get; set; }

        public override decimal CalculateDiscount()
        {
            return Price * 0.1m;
        }
    }

    public class Electronics : Product
    {
        public int Memory { get; set; }

        public override decimal CalculateDiscount()
        {
            return Price * 0.2m;
        }
    }

    public class Clothing : Product
    {
        public string Size { get; set; }

        public override decimal CalculateDiscount()
        {
            return Price * 0.15m;
        }
    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public List<IProduct> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public Action<string> StatusChanged { get; set; }

        public Order(int orderNumber, List<IProduct> products)
        {
            OrderNumber = orderNumber;
            Products = products;
            TotalPrice = 0;
            StatusChanged = (status) => Console.WriteLine($"Order #{OrderNumber} status: {status}");
        }
    }

    public class OrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            order.TotalPrice = 0;

            foreach (var product in order.Products)
            {
                order.TotalPrice += product.Price;
            }

            order.StatusChanged.Invoke("Processed");
        }
    }

    public class NotificationService
    {
        public void SendNotification(string status)
        {
            Console.WriteLine($"Notification: Your order has been {status}.");
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            var products = new List<IProduct>
            {
                new Book { Name = "Book 1", Price = 20, Pages = 200 },
                new Electronics { Name = "Laptop", Price = 1500, Memory = 16 },
                new Clothing { Name = "T-Shirt", Price = 15, Size = "L" }
            };

            var order = new Order(1, products);
            var orderProcessor = new OrderProcessor();
            orderProcessor.ProcessOrder(order);
        }
    }
}