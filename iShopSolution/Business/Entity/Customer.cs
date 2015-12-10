using System.Runtime.Serialization;

namespace Business.Entity
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string CodeId { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}