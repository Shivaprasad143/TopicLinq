using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqAssignment
{
    public class Product
    {
       public int ID;
       public string Name;
       public int Price;
       public int UnitsInStock;
        public Product(int ID, string name, int price, int unitsinstock)
        {
            this.ID = ID;
            this.Name = name;
            this.Price = price;
            this.UnitsInStock = unitsinstock;
        }

    }
    class Customer
    {
       public int ID;
       public string name;  
        public Customer(int ID, string name)
        {
            this.ID = ID;
            this.name = name;
        }
    }
    class Order
    {
       public int ID;
       public DateTime dateOrder;
       public Product product;
       public Customer customer;
        public Order(int ID, DateTime dateOrder, Product product, Customer customer)
        {
            this.ID = ID;
            this.dateOrder = dateOrder;
            this.product = product;
            this.customer = customer;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>(5);
            List<Customer> customers = new List<Customer>(5);
            List<Order> orders = new List<Order>(10);

            products.Add(new Product(1, "mobile", 100, 2));
            products.Add(new Product(2, "laptop", 200, 3));
            products.Add(new Product(3, "ipad", 100, 5));
            products.Add(new Product(4, "sunglass", 1000, 4));
            products.Add(new Product(5, "watch", 100, 1));

            customers.Add(new Customer(1, "shiva"));
            customers.Add(new Customer(2, "prasad"));
            customers.Add(new Customer(3, "kumar"));
            customers.Add(new Customer(4, "harsha"));
            customers.Add(new Customer(5, "niharika"));

            orders.Add(new Order(1, Convert.ToDateTime("1/25/2017"), products[1], customers[0]));
            orders.Add(new Order(2, Convert.ToDateTime("2/20/2016"), products[2], customers[1]));
            orders.Add(new Order(3, Convert.ToDateTime("3/31/2016"), products[3], customers[4]));
            orders.Add(new Order(4, Convert.ToDateTime("4/24/2016"), products[4], customers[1]));
            orders.Add(new Order(5, Convert.ToDateTime("5/7/2016"), products[0], customers[2]));
            orders.Add(new Order(6, Convert.ToDateTime("6/11/2016"), products[3], customers[3]));
            orders.Add(new Order(7, Convert.ToDateTime("7/17/2016"), products[2], customers[0]));
            orders.Add(new Order(8, Convert.ToDateTime("8/13/2017"), products[3], customers[3]));
            orders.Add(new Order(9, Convert.ToDateTime("9/28/2017"), products[4], customers[2]));
            orders.Add(new Order(10, Convert.ToDateTime("10/21/2016"), products[2], customers[1]));

            var ProductinStock = from value in products
                                 where value.UnitsInStock > 0 && value.Price > 100
                                 select value.Name;
            Console.WriteLine("Printing all product name in stock and Price greater than 100.: ");
            foreach (var product in ProductinStock)
            {
                Console.WriteLine(product);
            }

            var Value = from order in orders
                        group order by order.customer.ID into CG
                        select new
                        {
                            value = CG.Sum(o => o.product.Price),
                            key = CG.Key
                        };

            Console.WriteLine("Heighest to lowest value spent by the 5 customers:");
            foreach (var i in Value)
                Console.WriteLine(i);



            DateTime date = DateTime.Now;

            var buy = from order in orders
                      where DateTime.Compare(order.dateOrder, date.AddMonths(-1)) > 0
                      select order.customer.name;


            Console.WriteLine("Printing all customers who brought a product at least once in last month:");
            foreach (var bought in buy)
            {
                Console.WriteLine(bought);
            }


            var productList = from order in orders
                       group orders by order.product.ID into ProductGroup
                       select new
                       {
                           value = ProductGroup.Count(),
                           key = ProductGroup.Key
                       };

            Console.WriteLine("Printing Names of the product with number of times it was bought!");
            foreach (var t in productList)
                Console.WriteLine(t);
            
            Console.ReadLine();
       }
    }
}
