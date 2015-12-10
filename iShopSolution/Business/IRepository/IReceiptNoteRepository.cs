using System;
using System.Linq;
using Business.Entity;

namespace Business.IRepository
{
    public interface IReceiptNoteRepository
    {
        IQueryable<ReceiptNote> GetAll();
        void AddPending(ReceiptNote receiptNote);
        void Add(ReceiptNote receiptNote);
        void Update(ReceiptNote receiptNote);
        void Submit();
    }
}