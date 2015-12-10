using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Business.Entity;

namespace Service
{
    [ServiceContract]
    public interface IShopService
    {
        [OperationContract]
        Category[] GetAllCategories();

        [OperationContract]
        Product[] GetByCate(int cateId);

        [OperationContract]
        Product GetProduct(int proId);

        [OperationContract]
        Product[] Find(string productName, int cateId);

        [OperationContract]
        WeightedAverageUnitPrice FindPrice(int proId, DateTime date);

        [OperationContract]
        int ProgressOrder(DeliveryNote note);

        [OperationContract]
        DeliveryNote GetDelivery(int noteId);

        [OperationContract]
        byte GetStatusDelivery(int noteId);
    }
}
