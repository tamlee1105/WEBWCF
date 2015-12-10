using System;
using System.Collections.Generic;
using Business.Entity;

namespace Business.IRepository
{
    public interface IMyRepository
    {
        int GetUnitProductAllRepository(int proId);
        int GetUnitProductByRepository(int proId, int repo, out int id);

        MyRepo GetNewRepoProduct(int proId, int repo);
        IEnumerable<MyRepo> GetNewRepoProduct(int proId);

        MyRepo GetFirstItemReceipt(int proId, int repo);
        List<MyRepo> GetFirstItemReceipt(int proId);

        IEnumerable<MyRepo> GetRepoProduct(int proId);

        IEnumerable<MyRepo> GetRepoProduct(int proId,DateTime date);

        IEnumerable<MyRepo> GetRepoByDeliveryNoteDetails(int noteDetailsId);

        bool ProgressRepoWhenAddDelivery(DeliveryNoteDetail noteDetail, int noteId);
        void Add(MyRepo repo);
        void Update(int id,int remainUnit);
        void Submit();
    }
}