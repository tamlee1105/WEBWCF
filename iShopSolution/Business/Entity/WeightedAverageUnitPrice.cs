using System;
using System.Runtime.Serialization;

namespace Business.Entity
{
    [DataContract]
    public class WeightedAverageUnitPrice
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public decimal AverageUnitPrice { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int Units { get; set; }
        [DataMember]
        public int ProductId { get; set; }
        [DataMember]
        public Product Product { get; set; }
    }
}