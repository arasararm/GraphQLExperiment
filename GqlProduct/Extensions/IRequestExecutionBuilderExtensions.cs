using GqlProduct.Configuration;
using HotChocolate.Execution.Configuration;
using StackExchange.Redis;

namespace GqlProduct.Extensions
{
    public static class IRequestExecutionBuilderExtensions
    {
        public static IRequestExecutorBuilder CustomPublishSchemaDefinition(
        this IRequestExecutorBuilder builder,
        GraphQLConfiguration graphQlConfiguration)
        {
            if (graphQlConfiguration.Stitching!.Enabled)
            {
                builder.PublishSchemaDefinition(c => c
                    .SetName(graphQlConfiguration.ServiceName!)
                    .PublishToRedis(graphQlConfiguration.GatewayName!, sp => sp.GetRequiredService<ConnectionMultiplexer>()));
            }

            return builder;
        }
    }
}
