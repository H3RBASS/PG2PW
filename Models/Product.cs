public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OriginalPrice { get; set; }
    public string Image { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public bool InStock { get; set; } = true;
    public List<string> Tags { get; set; } = new();
}

public class CartItem
{
    public Product Product { get; set; } = new();
    public int Quantity { get; set; }
}

public class FilterModel
{
    public string SearchTerm { get; set; } = string.Empty;
    public string Category { get; set; } = "all";
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; } = 1000;
    public string SortBy { get; set; } = "name";
}