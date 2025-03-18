
namespace Catalog.API.Products.GetProductByCategory
{
    public record class GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await documentSession.Query<Product>().Where(p => p.Categories.Contains(query.category)).ToListAsync();



            return new GetProductByCategoryResult(products);
        }
    }
}
