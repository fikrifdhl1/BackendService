namespace BackendService.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }

    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
        public int? Stock { get; set; }
    }

    public class UpdateStockDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
