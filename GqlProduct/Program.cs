using EntityGraphQL.AspNet;
using Framework.Diagnostics.ExecutionEvents;
using GqlProduct.Configuration;
using GqlProduct.Extensions;
using GqlProduct.Models;
using GqlProduct.Services;
using GqlProduct.Types;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;


namespace GqlProduct
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var graphQlConfig = new GraphQLConfiguration();
            builder.Configuration.GetSection("GraphQL").Bind(graphQlConfig);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContextFactory<ProductContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ProductContext>(sp =>
            sp.GetRequiredService<IDbContextFactory<ProductContext>>().CreateDbContext());

            builder.Services.AddScoped(typeof(IService<Category>), typeof(CategoryService));
            builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));

            builder.Services
                .AddSingleton(ConnectionMultiplexer.Connect(graphQlConfig.Redis!.Endpoint))
                .AddGraphQLServer()
                .AddDiagnosticEventListener<CustomExecutionEventListener>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddMutationConventions(applyToAllMutations: true)
                .InitializeOnStartup()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .CustomPublishSchemaDefinition(graphQlConfig);

            var app = builder.Build();

            app.MapGraphQL();
            app.Run();
        }
    }
}
