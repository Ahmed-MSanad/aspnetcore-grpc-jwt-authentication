using Grpc.Core;
using Grpc.Net.Client;
using GrpcCRUDApplication.Protos;

namespace ConsoleAppClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7218");
            var client = new ProductProtoService.ProductProtoServiceClient(channel);

            var header = new Metadata
            {
                { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1NmJlZjU3YS1hMDcyLTQyYWUtYThhZi04NzI2YmY5YzhhMWMiLCJleHAiOjE3NzQzNzk1NzYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcyMTgiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjE4In0.LtNTZL5k2fr9GS3Ta4NQvFcZ_T6cL_W-lFYEAiGzPxw"}
            };

            var create1 = await client.CreateProductAsync(new CreateProductRequest
            {
                Name = "Product 4",
                Description = "This is Product 4",
                Price = 102.20
            }, header);
            Console.WriteLine(create1);

            Console.WriteLine(new string('#', 40));

            var create2 = await client.CreateProductAsync(new CreateProductRequest
            {
                Name = "Product 5",
                Description = "This is Product 5",
                Price = 632.15
            }, header);
            Console.WriteLine(create2);

            Console.WriteLine(new string('#', 40));

            var list = await client.ListProductsAsync(new Google.Protobuf.WellKnownTypes.Empty(), header);
            foreach (var p in list.Products)
                Console.WriteLine(p);

            Console.ReadKey();
        }
    }
}
