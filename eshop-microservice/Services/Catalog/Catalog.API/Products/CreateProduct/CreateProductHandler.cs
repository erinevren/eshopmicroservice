using BuildingBlocks.CQRS;
using Catalog.API.Models;
using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Name= request.Name, Categories= request.Categories, Description= request.Description, ImageFile=  request.ImageFile, Price= request.Price };
            documentSession.Store(product);
            await documentSession.SaveChangesAsync();
            return new CreateProductResult(product.Id);
        }
    }
}
