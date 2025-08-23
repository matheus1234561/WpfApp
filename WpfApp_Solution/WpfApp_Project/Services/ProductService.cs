using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WpfApp_Project.Models;

namespace WpfApp_Project.Services
{
    public class ProductService
    {
        public List<Product> products = new List<Product>();

        private const string caminhoXml = @"..\..\Data\Products.xml";

        public void SaveProduct(List<Product> products)
        {

            XDocument doc = new XDocument(new XElement("Products",
                products.ConvertAll(p => new XElement("Product",
                    new XElement("Id", p.Id),
                    new XElement("Name", p.Name),
                    new XElement("Code", p.Code),
                    new XElement("Price", p.Price)))));

            doc.Save(caminhoXml);
        }

        public List<Product> LoadProductFromXml()
        {

            XDocument doc = XDocument.Load(caminhoXml);

            if (doc.Descendants("Product").Any())
            {
                products = (from p in doc.Descendants("Product")
                            select new Product
                            {
                                Id = (int)p.Element("Id"),
                                Name = (string)p.Element("Name"),
                                Code = (string)p.Element("Code"),
                                Price = decimal.Parse(p.Element("Price").Value ?? "0")
                            }).ToList();
            }
            else
            {
                products = new List<Product>();
            }


            return products;
        }

        public void ProductEdit(Product editProdutc)
        {
            List<Product> products = LoadProductFromXml();

            var product = products.FirstOrDefault(p => p.Id == editProdutc.Id);

            if (product != null)
            {
                product.Name = editProdutc.Name;
                product.Code = editProdutc.Code;
                product.Price = editProdutc.Price;

                SaveProduct(products);
            }
        }

        public void ProductExclude(Product excludeProduct)
        {
            List<Product> products = LoadProductFromXml();

            var product = products.FirstOrDefault(p => p.Id == excludeProduct.Id);

            if (product != null)
            {
                products.Remove(product);

                SaveProduct(products);
            }
        }

        public int GenerateLastId()
        {
            List<Product> products = LoadProductFromXml();
            int proximoId = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            return proximoId;
        }
    }
}
