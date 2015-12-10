namespace Business.Entity
{
    public class ReceiptNoteDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ReceiptNoteId { get; set; }
        public ReceiptNote ReceiptNote { get; set; }

        public int Unit { get; set; }
        public decimal CostPrice { get; set; }
        public byte Repository { get; set; }
    }
}