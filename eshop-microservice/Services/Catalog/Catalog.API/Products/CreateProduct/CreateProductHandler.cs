namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required!");
            RuleFor(x => x.Categories).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageFile).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product price is required!");
        }
    }
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
