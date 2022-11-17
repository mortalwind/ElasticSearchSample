using ElasticSearchSample.API.Models;

using Nest;

namespace ElasticSearchSample.API.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(
        this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["ElasticSearchConfiguration:Uri"];
        var defaultIndex = configuration["ElasticSearchConfiguration:index"];

        var settings = new ConnectionSettings(new Uri(url)).BasicAuthentication("elastic", "9psHigdZINQaM0q=Ijbi")
            .PrettyJson()
            .DefaultIndex(defaultIndex);

        AddDefaultMappings(settings);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        CreateIndex(client, defaultIndex);
    }

    private static void AddDefaultMappings(ConnectionSettings settings)
    {
        settings
            .DefaultMappingFor<User>(m=>m);
    }

    private static void CreateIndex(IElasticClient client, string indexName)
    {
        var createIndexResponse = client.Indices.Create(indexName,
            index => index.Map<User>(x => x.AutoMap())
        );
    }

}

