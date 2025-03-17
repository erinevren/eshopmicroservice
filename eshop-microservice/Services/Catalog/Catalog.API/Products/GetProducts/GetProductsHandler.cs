using BuildingBlocks.CQRS;
using Marten;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await documentSession.Query<Product>().ToListAsync();
            return new GetProductResult(products);
        }
    }
}
