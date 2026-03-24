using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcCRUDApplication.Protos;

namespace GrpcCRUDApplication.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Description = "This is Product 1", Name = "Product 1", Price = 10.0 },
            new Product { Id = 2, Description = "This is Product 2", Name = "Product 2", Price = 20.0 },
            new Product { Id = 3, Description = "This is Product 3", Name = "Product 3", Price = 30.0 }
        };
        public override Task<Product?> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = _products.FirstOrDefault(p => p.Id == request.Id);

            if (product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found!"));
            else
                return Task.FromResult(product)!;
        }
        public override Task<ProductListResponse?> ListProducts(Empty request, ServerCallContext context)
        {
            var response = new ProductListResponse();
            response.Products.AddRange(_products);
            if (response.Products.Count == 0)
                throw new RpcException(new Status(StatusCode.NotFound, "No products found!"));
            return Task.FromResult(response)!;
        }
        public override Task<Product> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            var product = new Product
            {
                Id = _products.Count + 1,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };
            _products.Add(product);
            return Task.FromResult(product);
        }
        public override Task<Product> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var product = _products.FirstOrDefault(p => p.Id == request.Id);
            if(product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found!"));
            
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            return Task.FromResult(product);
        }
        public override Task<Empty> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            var product = _products.FirstOrDefault(p => p.Id == request.Id);
            if(product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Requested product not found!"));

            _products.Remove(product);
            return Task.FromResult(new Empty());
        }
    }
}
