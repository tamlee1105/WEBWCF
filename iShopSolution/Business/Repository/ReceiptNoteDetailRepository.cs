using System;
using System.Collections.Generic;
using System.Linq;
using Business.Database;
using Business.Entity;
using Business.IRepository;

namespace Business.Repository
{
    public class ReceiptNoteDetailRepository : IReceiptNoteDetailRepository
    {
        private ShopDataContext _data = ShopData.DataContext;

        #region Implementation of IReceiptNoteDetailRepository

        public void Add(ReceiptNoteDetail receiptNoteDetail)
        {
            var chitiet = receiptNoteDetail.ConvertToChiTietPhieuNhapHang();

            _data.ChiTietPhieuNhapHangs.InsertOnSubmit(chitiet);
            Submit();
            receiptNoteDetail.Id = chitiet.ID;
        }

        public void Update(ReceiptNoteDetail receiptNoteDetail)
        {
            var chitiet = _data.ChiTietPhieuNhapHangs.FirstOrDefault(ct => ct.ID == receiptNoteDetail.Id);
            if (chitiet == null) return;

            chitiet.SoLuong = receiptNoteDetail.Unit;
            chitiet.GiaVon = receiptNoteDetail.CostPrice;

        }

        public IEnumerable<ReceiptNoteDetail> GetByProduct(int proId, DateTime _from, DateTime _to)
        {
            var noteDetals = _data.ChiTietPhieuNhapHangs.Where(c => c.IDSanPham == proId &&
                c.PhieuNhapHang.ThoiGianNhap >= _from &&
                c.PhieuNhapHang.ThoiGianNhap <= _to);
            return !noteDetals.Any() ? null : noteDetals.Select(c => c.ConvertToReceiptNoteDetail());
        }

        public void Submit()
        {
            _data.SubmitChanges();
        }

        #endregion
    }
}