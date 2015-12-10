using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Business.Entity
{
    [DataContract]
    public class DeliveryNoteDetail
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int DeliveryNoteId { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public Product Product { get; set; }
        [DataMember]
        public int Unit { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public DeliveryNote DeliveryNote { get; set; }

        // extension

        private IEnumerable<MyRepo> _repos;
        [DataMember]
        public IEnumerable<MyRepo> Repos
        {
            get { return _repos ?? (_repos = new List<MyRepo>()); }
            set { _repos = value; }
        }
    }
}