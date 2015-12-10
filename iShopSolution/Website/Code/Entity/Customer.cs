using System;

namespace Website.Code.Entity
{
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CodeId { get; set; }
        public string Email { get; set; }
    }
}