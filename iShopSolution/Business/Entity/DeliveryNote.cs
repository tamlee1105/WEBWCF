using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Business.Entity
{
    [DataContract]
    public class DeliveryNote
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public bool Status { get; set; }

        private List<DeliveryNoteDetail> _noteDetails;
        [DataMember]
        public List<DeliveryNoteDetail> NoteDetails
        {
            get { return _noteDetails ?? (_noteDetails = new List<DeliveryNoteDetail>()); }
            set { _noteDetails = value; }
        }

        // extension
        public string StrStatus
        {
            get { return Status ? "Processed" : "Waiting"; }
        }
    }
}