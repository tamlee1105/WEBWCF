using System;
using System.Collections.Generic;
using System.Linq;
using Business.Repository;

namespace App.Report
{
    public class ReportView
    {
      
        public static IEnumerable<ReceiptDelivery> GetData(int cateId, DateTime _from,DateTime _to)
        {
            var list = new List<ReceiptDelivery>();
            var productRepository = new ProductRepository();
            var receiptDetailsRepository = new ReceiptNoteDetailRepository();
            var deliveryDetailsRepository = new DeliveryNoteDetailRepository();

            var products = cateId == 0 ? productRepository.GetAll() : productRepository.GetByCate(cateId);
            foreach (var p in products)
            {
                var receiptDetails = receiptDetailsRepository.GetByProduct(p.Id, _from,_to);
                var deliveryDetails = deliveryDetailsRepository.GetByProduct(p.Id, _from, _to);
                if (receiptDetails != null)
                    list.AddRange(receiptDetails
                                      .Select(detail => new ReceiptDelivery
                                                            {
                                                                Name = p.Name,
                                                                Date = detail.ReceiptNote.Date,
                                                                Unit = detail.Unit,
                                                                IsReceipt = true
                                                            }));
                if (deliveryDetails != null)
                    list.AddRange(deliveryDetails
                                      .Select(detail => new ReceiptDelivery
                                                            {
                                                                Name = p.Name,
                                                                Date = detail.DeliveryNote.Date,
                                                                Unit = detail.Unit,
                                                                IsReceipt = false
                                                            }));
            }

            return list;
        }
    }
    public class ReceiptDelivery
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Unit { get; set; }
        public bool IsReceipt { get; set; }
    }
}