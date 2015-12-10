using System;
using System.Collections.Generic;
using Business.Entity;

namespace Business.IRepository
{
    public interface IReceiptNoteDetailRepository
    {
        void Add(ReceiptNoteDetail receiptNoteDetail);
        void Update(ReceiptNoteDetail receiptNoteDetail);

        IEnumerable<ReceiptNoteDetail> GetByProduct(int proId, DateTime _from, DateTime _to);
        void Submit();
    }
}