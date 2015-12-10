// Copyright: 2012 
// Author: Minh Vu - YoungJ
// File name: Extension.cs
// Solution: iShopSolution
// Project: Website
// Time: 8:55 PM 15/05/2012

using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Website.Code.Entity;

namespace Website.Code
{
    public static class Extension
    {
        public static bool SaveXml(XDocument xdoc, string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                xdoc.Save(stream);
                return true;
            }
        }

        public static XDocument LoadXml(string filename)
        {
            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return XDocument.Load(stream);
            }
        }

        public static XElement ConvertToXElementCart(this Cart cart)
        {
            var eDetails = new XElement("orderdetails",
                                        cart.OrdersDetails.Select(
                                            o =>
                                            new XElement("detail", new XElement("productId", o.ProductId),
                                                                     new XElement("productName", o.ProductName),
                                                                     new XElement("productImage", o.ProductImage.UrlEncode()),
                                                                     new XElement("productPrice", o.ProductPrice),
                                                                     new XElement("cateId", o.CateId),
                                                                     new XElement("cateName", o.CateName),
                                                                     new XElement("unit", o.Unit))));

            var eCustomer = new XElement("customer", new XElement("codeid", cart.Customer.CodeId),
                                                     new XElement("name", cart.Customer.Name),
                                                     new XElement("address", cart.Customer.Address),
                                                     new XElement("phone", cart.Customer.Phone),
                                                     new XElement("email", cart.Customer.Email));

            var eCart = new XElement("cart", new XElement("id", cart.Id),
                                             new XElement("deliveryId", 0),
                                             new XElement("status", cart.Status),
                                             new XElement("date", cart.Date),
                                             new XElement("totalprice", cart.TotalPrice),
                                             eDetails, eCustomer);
            return eCart;
        }

        public static string GetElementValue(this XElement element, string elementName)
        {
            var xElement = element.Element(elementName);
            return xElement != null ? xElement.Value : string.Empty;
        }

        public static string GetAttributeValue(this XElement element, string elementName)
        {
            var xElement = element.Attribute(elementName);
            return xElement != null ? xElement.Value : string.Empty;
        }

        public static string UrlEncode(this string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string UrlDecode(this string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        public static string Encrypt(this string text)
        {
            var _md5Hasher = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(text);
            bs = _md5Hasher.ComputeHash(bs);
            var s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }
    }
}