namespace EpisconApi.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
