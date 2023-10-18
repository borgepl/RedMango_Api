namespace DataAccess.Data.Domain
{
    public class CartItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }

    }
}
