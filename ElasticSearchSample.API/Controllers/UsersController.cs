using ElasticSearchSample.API.Abstractions;
using ElasticSearchSample.API.Models;
using ElasticSearchSample.API.Services;

using Microsoft.AspNetCore.Mvc;

using Nest;

namespace ElasticSearchSample.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly IDataRepository<User> _repository;
	private readonly UserSearchService _elasticSearch; 
    private readonly IElasticClient _elasticClient;
    public UsersController(IDataRepository<User> repository, IElasticClient elasticClient)
	{
		_repository = repository;
		_elasticClient = elasticClient;
		_elasticSearch = new UserSearchService(_elasticClient);
    }

	[HttpGet]
	public async Task<ActionResult> Get([FromQuery]string name, string? lastname, int pageIndex, int count) {

		if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname)) {
			return BadRequest("Search criteria is empty");
		}
		var searchResult = await _elasticSearch.SearchAsync(name, lastname, pageIndex, count);

		if (searchResult.Any())
		{
			return Ok(searchResult);
		}
		else
		{
			var result = _repository.SearchAsync(x =>
				(x.Name.Equals(name) || string.IsNullOrEmpty(name)) &&
				(x.Lastname.Equals(lastname) || string.IsNullOrEmpty(lastname))).Result;

			foreach (var item in result)
			{
				await _elasticSearch.IndexAsync(item);
			}

            return Ok(result);
        }
		
	}
}
