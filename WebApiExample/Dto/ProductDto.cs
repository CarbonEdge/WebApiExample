namespace WebApiExample.Dto
{
    public class ProductDto
    {
        public List<Items>? products { get; set; }
    }

    public class Items
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public double Points { get; set; }
    }
}
