using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp_Project.Models;
using WpfApp_Project.Models.Enum;

namespace WpfApp_Project.Services
{
    public class OrderService
    {
        public List<Order> orders = new List<Order>();

        private const string caminhoXml = @"..\..\Data\Orders.xml";


        public void SaveOrder(List<Order> orders)
        {

            XDocument doc = new XDocument(new XElement("Orders",
                orders.ConvertAll(o => new XElement("Order",
                    new XElement("Id", o.Id),
                    new XElement("Person", 
                                    new XElement("Id", o.Person.Id),
                                    new XElement("Name", o.Person.Name),
                                    new XElement("CPF", o.Person.CPF)),
                    new XElement("Products", o.Products.Select(p => new XElement("Product",
                                    new XElement("Id", p.Id),
                                    new XElement("Code", p.Code),
                                    new XElement("Name", p.Name),
                                    new XElement("Price", Math.Round(p.Price, 2))))),
                    new XElement("DateOfSale", o.DateOfSale),
                    new XElement("TotalPrice", o.TotalPrice),
                    new XElement("PaymentMethod", o.PaymentMethod),
                    new XElement("Status", o.Status)))));

            doc.Save(caminhoXml);
        }


        public List<Order> LoadOrderFromXml()
        {

            XDocument doc = XDocument.Load(caminhoXml);

            if (doc.Descendants("Order").Any())
            {
                orders = (from o in doc.Descendants("Order")
                           select new Order
                           {
                               Id = (int)o.Element("Id"),
                               Person = new Person 
                               { 
                                   Id = (int)o.Element("Person").Element("Id"),
                                   Name = (string)o.Element("Person").Element("Name"),
                                   CPF = (string)o.Element("Person").Element("CPF"),
                                   Address = (string)o.Element("Person").Element("Address"),
                               },
                               Products = (from prod in o.Descendants("Products") select new Product
                               {
                                   Id = (int)prod.Element("Id"),
                                   Code = (string)prod.Element("Code"),
                                   Name = (string)prod.Element("Name"),
                                   Price = decimal.Parse(prod.Element("Price").Value ?? "0"),
                               }).ToList(),
                               DateOfSale = (DateTime)o.Element("DateOfSale"),
                               TotalPrice = (decimal)o.Element("TotalPrice"),
                               PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod),(string)o.Element("PaymentMethod")),
                               Status = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), (string)o.Element("Status"))
                           }).ToList();
            }
            else
            {
                orders = new List<Order>();
            }


            return orders;
        }

        public void OrderEdit(Order editOrder)
        {
            List<Order> orders = LoadOrderFromXml();

            var order = orders.FirstOrDefault(o => o.Id == editOrder.Id);

            if (order != null)
            {
                order.Person = editOrder.Person;
                order.Products = editOrder.Products;
                order.DateOfSale = editOrder.DateOfSale;
                order.TotalPrice = editOrder.TotalPrice;
                order.PaymentMethod = editOrder.PaymentMethod;
                order.Status = editOrder.Status;

                SaveOrder(orders);
            }
        }

        public void OrderExclude(Order excludeOrder)
        {
            List<Order> orders = LoadOrderFromXml();

            var Order = orders.FirstOrDefault(o => o.Id == excludeOrder.Id);

            if (Order != null)
            {
                orders.Remove(Order);

                SaveOrder(orders);
            }
        }

        public int GenerateLastId()
        {
            List<Order> orders = LoadOrderFromXml();
            int proximoId = orders.Count > 0 ? orders.Max(p => p.Id) + 1 : 1;
            return proximoId;
        }
    }
}
