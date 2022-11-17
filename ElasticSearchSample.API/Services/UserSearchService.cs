using ElasticSearchSample.API.Abstractions;
using ElasticSearchSample.API.Models;

using Nest;

namespace ElasticSearchSample.API.Services;

public class UserSearchService : IElasticSearchService<User>
{
    private readonly IElasticClient _client;

    public UserSearchService(IElasticClient client)
    {
        _client = client;
    }

    public async Task DeleteAsync(string id)
    {
        await _client.DeleteAsync<User>(id);
    }

    public async Task<User> GetAsync(string id)
    {
        var response = await _client.GetAsync<User>(id);
        return response.Source;
    }

    public async Task<IndexResponse> IndexAsync(User document)
    {
        var result = await _client.IndexDocumentAsync<User>(document);
        return result;
    }

    public async Task<IEnumerable<User>> SearchAsync(string? name, string? lastname, int pageNumber = 0, int viewCount = 25)
    {
        var response = await _client.SearchAsync<User>(s => s.Index(typeof(User).Name)
            .From(pageNumber)
            .Size(viewCount)
            .Query(q => q.Term(t => t.Name, name))
        );
        return response.Documents.ToList();
    }

    public async Task<User> UpdateAsync(string id, User document)
    {
        var result = await _client.UpdateAsync<User, object>(id, u => u.Doc(document));
        if (result.IsValid)
        {
            return document;
        }
        return null;
    }
}
