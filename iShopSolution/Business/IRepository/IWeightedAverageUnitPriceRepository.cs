using System;
using Business.Entity;

namespace Business.IRepository
{
    public interface IWeightedAverageUnitPriceRepository
    {
        decimal GetPrice(int proId);
        decimal GetPriceByDate(int proId, DateTime date);
        WeightedAverageUnitPrice GetByDate(int proId, DateTime date);
        void Add(ReceiptNoteDetail noteDetail);
        void Add(WeightedAverageUnitPrice averageUnitPrice);
        WeightedAverageUnitPrice Find(int proId, DateTime date);
        void Submit();
    }
}