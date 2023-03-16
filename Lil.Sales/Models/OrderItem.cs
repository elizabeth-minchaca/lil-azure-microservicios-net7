namespace Lil.Sales.Models
{
    public class OrderItem
    {
        public string OrderId { get; set; }

        public int Id { get; set; }

        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
