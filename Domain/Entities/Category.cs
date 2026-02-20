
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ParentId{get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Category>  Children{get;set;}=[];
    public List<Product> Products { get; set; } = [];
}
