
using Microsoft.EntityFrameworkCore;
using System.Net;
public class WarehouseService(ApplicationDbContext dbContext):IWarehouseService
{
    private readonly ApplicationDbContext context = dbContext;
//stock-складские остатки
    public async Task<Response<string>> AddStockAsync(int warehouseId, int productId, int quantity)
    {
        var product = await context.Products.FirstOrDefaultAsync(o=>o.Id==productId);
       if (product == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Product not found");
        }
       var warehouse = await context.Warehouses
        .FirstOrDefaultAsync(o => o.Id == warehouseId);

    if (warehouse == null)
    {
        return new Response<string>(HttpStatusCode.NotFound, "Warehouse not found");
    }

    var warehouseProduct = await context.WarehouseProducts
        .FirstOrDefaultAsync(o => o.ProductId == productId && o.WarehouseId == warehouseId);

    if (warehouseProduct == null)
    {
        warehouseProduct = new WarehouseProduct
        {
            ProductId = productId,
            WarehouseId = warehouseId,
            Quantity = quantity
        };

        await context.WarehouseProducts.AddAsync(warehouseProduct);
    }
    else
    {
        warehouseProduct.Quantity += quantity;
    }

    await context.SaveChangesAsync();

    return new Response<string>(HttpStatusCode.OK, "Stock added successfully");


    }

    public async Task<Response<int>> CheckStockAsync(int productId)
    {
         var product = await context.Products.FirstOrDefaultAsync(o => o.Id == productId);
    if (product == null)
    {
        return new Response<int>(HttpStatusCode.NotFound, "Product not found");
    }

    var totalStock = await context.WarehouseProducts
        .Where(o => o.ProductId == productId)
        .SumAsync(o => o.Quantity);
    return new Response<int>(HttpStatusCode.OK, "Stock retrieved", totalStock);
    }

    public async Task<Response<string>> DecreaseStockAsync(int productId, int quantity)
    {
         var warehouseProduct = await context.WarehouseProducts.Where(o => o.ProductId == productId).OrderByDescending(o => o.Quantity)
        .FirstOrDefaultAsync();

    if (warehouseProduct == null)
    {
        return new Response<string>(HttpStatusCode.NotFound, "Product not found in warehouses");
    }
    if (warehouseProduct.Quantity < quantity)
    {
        return new Response<string>(HttpStatusCode.BadRequest, "Not enough stock");
    }
    warehouseProduct.Quantity -= quantity;
    await context.SaveChangesAsync();
    return new Response<string>(HttpStatusCode.OK, "Stock decreased successfully");
    }
}