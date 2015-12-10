using System;
using System.Collections.Generic;
using Business.Entity;

namespace Business.IRepository
{
    public interface IDeliveryNoteDetailRepository
    {
        void Add(DeliveryNoteDetail noteDetail);
        void UpdateStatus(int noteDetailId);
        IEnumerable<DeliveryNoteDetail> Find(int deliveryNoteId);
        IEnumerable<DeliveryNoteDetail> GetByProduct(int proId, DateTime _from, DateTime _to);
        void Submit();
    }
}