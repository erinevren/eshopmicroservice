
namespace Catalog.API.Products.GetProductByCategory
{
    public record class GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResult>;
    public record class GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (ISender sender, string category) =>
            {
                var result = await sender.Send(new GetProductByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            });
        }
    }
}
