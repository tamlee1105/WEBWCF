using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Business.Database;
using Business.Entity;
using Business.IRepository;

namespace Business.Repository
{
    public class DeliveryNoteRepository : IDeliveryNoteRepository
    {
        private ShopDataContext _data = ShopData.DataContext;

        #region Implementation of IDeliveryNoteRepository

        public IEnumerable<DeliveryNote> GetAll()
        {
            var data = new ShopDataContext();
            return data.PhieuDatHangs.Select(p => p.ConvertToDeliveryNote());
        }

        public void Add(DeliveryNote note)
        {
            note.Date = DateTime.Now;
            var phieu = note.ConvertToPhieuDatHang();
            _data.PhieuDatHangs.InsertOnSubmit(phieu);
            Submit();
            note.Id = phieu.ID;
        }

        public DeliveryNote Find(int noteId)
        {
            var item = _data.PhieuDatHangs.FirstOrDefault(p => p.ID == noteId);
            if (item == null) return null;
            return item.ConvertToDeliveryNote();
        }

        public void UpdateStatus(int noteId)
        {
            var current = _data.PhieuDatHangs.FirstOrDefault(p => p.ID == noteId);
            if (current == null) return;
            current.Duyet = true;
            Submit();
        }

        public int ProgressShoppingCart(DeliveryNote note)
        {
            note.Status = false;
            Add(note);
            var noteDetailsRepository = new DeliveryNoteDetailRepository();
            var repository = new MyRepository();
            var success = 0;
            foreach (var detail in note.NoteDetails)
            {
                detail.Status = false;
                detail.DeliveryNoteId = note.Id;
                noteDetailsRepository.Add(detail);

                if (!repository.ProgressRepoWhenAddDelivery(detail, note.Id)) continue;
                success++;
                noteDetailsRepository.UpdateStatus(detail.Id);
            }
            if (success == note.NoteDetails.Count)
                UpdateStatus(note.Id);
            return note.Id;
        }

        public bool ProgressWaiting(int noteId)
        {
            try
            {
                var note = Find(noteId);
                var noteDetailsRepository = new DeliveryNoteDetailRepository();
                var repository = new MyRepository();
                var success = 0;

                foreach (var detail in note.NoteDetails
                    .Where(detail => repository
                        .ProgressRepoWhenAddDelivery(detail, note.Id)))
                {
                    success++;
                    noteDetailsRepository.UpdateStatus(detail.Id);
                }
                if (success == note.NoteDetails.Count)
                    UpdateStatus(note.Id);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Progress waiting: " + exception.Message);
            }
            return false;

        }

        public byte GetStatusDelivery(int noteId)
        {
            var item = _data.PhieuDatHangs.FirstOrDefault(c => c.ID == noteId);
            if (item == null) return 1;

            if (item.Duyet == null) return 1;

            if ((bool)item.Duyet) return 3;
            return 2;

        }

        public void Submit()
        {
            _data.SubmitChanges();
        }

        #endregion



    }
}