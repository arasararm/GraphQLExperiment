using Framework.Diagnostics.ExecutionEvents;
using GqlCustomer.Configuration;
using GqlCustomer.Extensions;
using GqlCustomer.Models;
using GqlCustomer.Services;
using GqlCustomer.Types;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace GqlCustomer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var graphQlConfig = new GraphQLConfiguration();
            builder.Configuration.GetSection("GraphQL").Bind(graphQlConfig);
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContextFactory<CustomerContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<CustomerContext>(sp =>

            sp.GetRequiredService<IDbContextFactory<CustomerContext>>().CreateDbContext());

            builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            builder.Services
                .AddSingleton(ConnectionMultiplexer.Connect(graphQlConfig.Redis!.Endpoint))
                .AddGraphQLServer()
                .AddDiagnosticEventListener<CustomExecutionEventListener>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddMutationConventions(applyToAllMutations: true)
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions()
                .InitializeOnStartup()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .CustomPublishSchemaDefinition(graphQlConfig);

            var app = builder.Build();

            app.UseRouting();
            app.UseWebSockets();

            app.MapGraphQL();
            app.Run();
        }
    }
}
