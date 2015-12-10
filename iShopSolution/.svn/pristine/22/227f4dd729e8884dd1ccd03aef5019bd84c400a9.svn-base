// Copyright: 2012 
// Author: Minh Vu - YoungJ
// File name: Repository.cs
// Solution: iShopSolution
// Project: Website
// Time: 6:32 PM 13/05/2012

using System;
using System.Diagnostics;
using System.Linq;
using Website.Code.Entity;
using Website.ShopService;
using Customer = Website.Code.Entity.Customer;

namespace Website.Code
{
    public class Repository
    {
        public static double XPercent { get; set; }

        public static Product[] GetProductsByCate(int cateId)
        {
            XPercent = Utils.GetPercent();
            var service = Utils.Service;
            var list = service.GetByCate(cateId);
            foreach (var product in list)
            {
                product.Price = (decimal)(((double)product.Price) * (1 + XPercent));
                product.StrPrice = string.Format("{0:0,0 VND}", product.Price);
            }
            return list;
        }

        public static Product GetProduct(int proId)
        {
            XPercent = Utils.GetPercent();
            try
            {
                var service = Utils.Service;
                var product = service.GetProduct(proId);
                product.Price = (decimal)(((double)product.Price) * (1 + XPercent));
                product.StrPrice = string.Format("{0:0,0 VND}", product.Price);
                return product;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Repository - Get Product: " + exception.Message);
                return null;
            }

        }

        public static Product[] Find(string proName, int cateId)
        {
            XPercent = Utils.GetPercent();
            var service = Utils.Service;
            var list = service.Find(proName, cateId);
            foreach (var product in list)
            {
                product.Price = (decimal)(((double)product.Price) * (1 + XPercent));
                product.StrPrice = string.Format("{0:0,0 VND}", product.Price);
            }
            return list;
        }

        public static bool SendShoppingCart(Cart cart)
        {
            try
            {
                var service = Utils.Service;
                var listDetails = cart.OrdersDetails
                    .Select(detail => new DeliveryNoteDetail
                                          {
                                              ProductId = detail.ProductId,
                                              Unit = detail.Unit
                                          }).ToList();
                var cus = new ShopService.Customer
                              {
                                  Name = cart.Customer.Name,
                                  Address = cart.Customer.Address,
                                  Phone = cart.Customer.Phone,
                                  Email = cart.Customer.Email,
                                  CodeId = cart.Customer.CodeId
                              };
                var note = new DeliveryNote
                               {
                                   NoteDetails = listDetails.ToArray(),
                                   Customer = cus
                               };

                var deliveyId = service.ProgressOrder(note);

                Utils.UpdateStatusOrder(cart.Id, deliveyId);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Send shopping cart: " + exception.Message);
                return false;
            }

        }

        public static Cart GetDelivery(int deliveryId)
        {
            try
            {
                var service = Utils.Service;
                var delivery = service.GetDelivery(deliveryId);
                var cart = new Cart
                               {
                                   DeliveryNoteId = delivery.Id,
                                   Date = delivery.Date,
                                   Customer = new Customer {Id = delivery.CustomerId},
                                   OrdersDetails = delivery.NoteDetails
                                       .Select(d => new OrderDetail {Id = d.Id, ProductId = d.ProductId, Unit = d.Unit})
                                       .ToList()
                               };
                foreach (var detail in cart.OrdersDetails)
                {
                    var product = GetProduct(detail.ProductId);
                    detail.ProductName = product.Name;
                    detail.ProductImage = product.ImageHost;
                    detail.ProductPrice = product.Price;
                    detail.CateId = product.Category.Id;
                    detail.CateName = product.Category.Name;

                    var notedetails = delivery.NoteDetails.FirstOrDefault(n => n.Id == detail.Id);
                    if (notedetails == null || notedetails.Repos == null) continue;
                    for (var i = 1; i <= 2; i++)
                    {
                        var repo1 = notedetails.Repos.Where(r => r.Repository == i).ToList();
                        if (repo1.Any())
                            detail.Stocks.Add(new Stock { Id = i, Unit = repo1.Sum(r => r.ReceiptUnit) });
                    }
                }
                return cart;

            }
            catch (Exception exception)
            {
                Debug.WriteLine("Get delivery: " + exception.Message);
                return null;
            }
        }

        public static byte GetStatusOrder(int deliveryId)
        {
            try
            {
                var service = Utils.Service;
                var status = service.GetStatusDelivery(deliveryId);
                return status;

            }
            catch (Exception exception)
            {
                Debug.WriteLine("Get status order: " + exception.Message);
                return 1;
            }

        }
    }
}