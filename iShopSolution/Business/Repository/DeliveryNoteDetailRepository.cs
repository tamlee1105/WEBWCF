using System;
using System.Collections.Generic;
using System.Linq;
using Business.Database;
using Business.Entity;
using Business.IRepository;

namespace Business.Repository
{
    public class DeliveryNoteDetailRepository : IDeliveryNoteDetailRepository
    {
        private ShopDataContext _data = ShopData.DataContext;
        #region Implementation of IDeliveryNoteDetailRepository

        public void Add(DeliveryNoteDetail noteDetail)
        {
            //noteDetail.Status = false;
            var ctpDatHang = noteDetail.ConvertToChiTietPhieuDatHang();
            _data.ChiTietPhieuDatHangs.InsertOnSubmit(ctpDatHang);
            Submit();
            noteDetail.Id = ctpDatHang.ID;
        }

        public void UpdateStatus(int noteDetailId)
        {
            var current = _data.ChiTietPhieuDatHangs.FirstOrDefault(no => no.ID == noteDetailId);
            if (current == null) return;
            current.TrangThai = true;

            Submit();
        }

        public IEnumerable<DeliveryNoteDetail> Find(int deliveryNoteId)
        {
            var noteDetails = from c in _data.ChiTietPhieuDatHangs
                              where c.IDDatHang == deliveryNoteId
                              select c.ConvertToDeliveryNoteDetails();
            return noteDetails;
        }

        public IEnumerable<DeliveryNoteDetail> GetByProduct(int proId, DateTime _from, DateTime _to)
        {
            var noteDetals = _data.ChiTietPhieuDatHangs
                .Where(c => c.IDSanPham == proId &&
                            c.PhieuDatHang.ThoiGianDatHang >= _from
                            && c.PhieuDatHang.ThoiGianDatHang <= _to);
            return !noteDetals.Any() ? null : noteDetals.Select(c => c.ConvertToDeliveryNoteDetails());
        }

        public void Submit()
        {
            _data.SubmitChanges();
        }

        #endregion
    }
}