using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Code.Entity;

namespace Website.Code
{
    public class ShoppingCart
    {

        #region Shopping cart
        public Cart Cart { get; set; }

        public ShoppingCart()
        {
            if (HttpContext.Current.Session["cart"] == null)
            {
                Cart = new Cart();
                HttpContext.Current.Session["cart"] = Cart;
            }
            else
            {
                Cart = (Cart)HttpContext.Current.Session["cart"];
            }
        }


        public void Add(int proId, int Unit)
        {

            // checking product in cart...
            var odrDetail = Cart.OrdersDetails.Find(o => o.ProductId == proId);
            if (odrDetail == null)
            {
                // get info product
                var product = Repository.GetProduct(proId);
                if (product == null) return;
                odrDetail = new OrderDetail
                {
                    Unit = Unit,

                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImage = product.ImageHost,
                    ProductPrice = product.Price,
                    CateId = product.Category.Id,
                    CateName = product.Category.Name
                };

                Cart.OrdersDetails.Add(odrDetail);
            }
            else
            {
                // icreament unit and calc total prince
                odrDetail.Unit += Unit;
            }


        }

        public void SetUnit(int proId, int Unit)
        {
            if (Unit == 0)
                Remove(proId);
            var current = Cart.OrdersDetails.Find(o => o.ProductId == proId);
            if (current == null) return;
            current.Unit = Unit;
        }

        private void Remove(int proId)
        {
            var current = Cart.OrdersDetails.Find(o => o.ProductId == proId);
            if (current == null) return;
            Cart.OrdersDetails.Remove(current);
        }

        public void Clear()
        {
            Cart.OrdersDetails.Clear();
        }


        public void Add()
        {
            Cart.Status = 1;
            Cart.Date = DateTime.Now;
            Cart.TotalPrice = GetTotalPriceCart();
            Utils.SaveShoppingCart(Cart);
            HttpContext.Current.Session["cart"] = null;
        }

        public decimal GetTotalPriceCart()
        {
            var total = Cart.OrdersDetails.Sum(o => o.Unit * o.ProductPrice);
            return total;
        }
        #endregion


        #region Manager order
        public static IEnumerable<Cart>  GetAllOrder()
        {
            return Utils.GetAllShoppinCart();
        }

        #endregion




    }
}