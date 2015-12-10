using System;

namespace Website.Code.Entity
{
    [Serializable]
    public class Stock
    {
        public int Id { get; set; }
        public string Name
        {
            get
            {
                switch (Id)
                {
                    case 1:
                        return "A";
                    case 2:
                        return "B";
                    default:
                        return "Không xác định";
                }
            }
        }
        public int Unit { get; set; }
    }
}