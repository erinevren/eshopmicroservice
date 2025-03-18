using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product not found with id: " + id)
        {

        }
    }
}
