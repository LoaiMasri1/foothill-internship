namespace ims.Models
{
    public class Product
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        override public string ToString()
        {
            return $"Name: {Name}, Price: {Price}, Quantity: {Quantity}";
        }

        override public bool Equals(object? obj)
        {
            if (obj is Product product)
            {
                return Name == product.Name && Price == product.Price && Quantity == product.Quantity;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Price.GetHashCode() ^ Quantity.GetHashCode();
        }
    }
}