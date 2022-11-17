using ElasticSearchSample.API.Models;

using System.Linq.Expressions;

namespace ElasticSearchSample.API.Abstractions;

/// <summary>
/// We use this repository for all entities.
/// <para>Generic Repository Design Pattern</para>
/// </summary>
/// <typeparam name="T">Entity Object</typeparam>
public interface IDataRepository<T> where T: BaseEntity
{
    /// <summary>
    /// Adds new entity
    /// </summary>
    /// <param name="entity">New entity</param>
    /// <returns></returns>
    Task<T?> AddAsync(T entity);

    /// <summary>
    /// Gets a specified entity
    /// </summary>
    /// <param name="id">Entity's unique ID</param>
    /// <returns></returns>
    Task<T?> GetAsync(Guid id);

    /// <summary>
    /// Updates an exist entity
    /// </summary>
    /// <param name="entity">Exist entity</param>
    /// <returns></returns>
    Task<T?> UpdateAsync(T entity);

    /// <summary>
    /// Finds entities by expression
    /// </summary>
    /// <param name="expression">Search expression</param>
    /// <returns></returns>
    Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression);

}
