using System;
using System.Collections.Generic;
using System.Linq;

namespace Website.Code.Entity
{
    [Serializable]
    public class Cart
    {
        public int Id { get; set; }
        public int DeliveryNoteId { get; set; }
        //public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public byte Status { get; set; }

        public decimal TotalPrice { get; set; }

        private List<OrderDetail> _orderDetails;
        public List<OrderDetail> OrdersDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            set { _orderDetails = value; }
        }

        //extension
        public int TotalProduct
        {
            get { return OrdersDetails.Count; }
        }

        public int TotalUnit
        {
            get { return OrdersDetails.Sum(o => o.Unit); }
        }

    }
}