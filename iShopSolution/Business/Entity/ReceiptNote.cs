using System;
using System.Collections.Generic;

namespace Business.Entity
{
    public class ReceiptNote
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public string StrStatus
        {
            get { return Status ? "Checked" : "Pending"; }
        }

        private List<ReceiptNoteDetail> _noteDetails;
        public List<ReceiptNoteDetail> ReceiptNoteDetails
        {
            get { return _noteDetails ?? (_noteDetails = new List<ReceiptNoteDetail>()); }
            set { _noteDetails = value; }
        }

        /*
         * if product in repository ins't enought
         * add one receipt note with status pending and remember this delivery
         */

        public int DeliveryNoteId { get; set; }
    }
}