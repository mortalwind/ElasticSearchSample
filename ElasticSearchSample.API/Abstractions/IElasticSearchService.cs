using Nest;

namespace ElasticSearchSample.API.Abstractions;

/// <summary>
/// ElasticSearch service
/// </summary>
/// <typeparam name="T">Result document</typeparam>
public interface IElasticSearchService<T> where T: class
{
    /// <summary>
    /// Create an index for new document
    /// </summary>
    /// <param name="document">Document</param>
    /// <returns></returns>
    Task<IndexResponse> IndexAsync(T document);

    /// <summary>
    /// Gets a specified document 
    /// </summary>
    /// <param name="id">Document's unique ID</param>
    /// <returns></returns>
    Task<T> GetAsync(string id);

    /// <summary>
    /// Searchs documents
    /// </summary>
    /// <param name="name">User's name</param>
    /// <param name="lastname">User's lastname</param>
    /// <param name="pageNumber">Paging page index</param>
    /// <param name="viewCount">How many documents will list?</param>
    /// <returns></returns>
    Task<IEnumerable<T>> SearchAsync(string? name, string? lastname, int pageNumber = 0, int viewCount = 25);


    /// <summary>
    /// Updates an exist document
    /// </summary>
    /// <param name="document">Exist document</param>
    /// <param name="id">Document's unique ID</param>
    /// <returns></returns>
    Task<T> UpdateAsync(string id, T document);

    /// <summary>
    /// Deletes an indexed document
    /// </summary>
    /// <param name="id">Document's unique ID</param>
    /// <returns></returns>
    Task DeleteAsync(string id);

}
