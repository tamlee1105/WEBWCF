using System;
using System.Collections.Generic;

namespace Website.Code.Entity
{
    [Serializable]
    public class OrderDetail
    {

        // details order
        public int Id { get; set; }
        public int Unit { get; set; }


        // product
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int CateId { get; set; }
        public string CateName { get; set; }

        // if Order is progress. details order is has detail product imported in where repository and how many unit

        private List<Stock> _stock;
        public List<Stock> Stocks { get { return _stock ?? (_stock = new List<Stock>()); } set { _stock = value; } }

        //extension

        public string StrPrice { get { return string.Format("{0:0,0 VND}", ProductPrice); } }

        public string StrTotalPrice { get { return string.Format("{0:0,0 VND}", ProductPrice * Unit); } }



    }
}