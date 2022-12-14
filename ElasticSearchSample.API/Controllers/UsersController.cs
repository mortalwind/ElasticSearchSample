using ElasticSearchSample.API.Abstractions;
using ElasticSearchSample.API.Models;
using ElasticSearchSample.API.Services;

using Microsoft.AspNetCore.Mvc;

using Nest;

using System.Xml.Linq;

namespace ElasticSearchSample.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	private readonly UserSearchService _elasticSearch; 
    private readonly IElasticClient _elasticClient;
    public UsersController(IElasticClient elasticClient)
	{
		_elasticClient = elasticClient;
		_elasticSearch = new UserSearchService(_elasticClient);
    }


	[HttpPost]
	public async Task<ActionResult> Post([FromBody] User newUser) {

		var response = await _elasticSearch.IndexAsync(newUser);
		
		if (response.IsValid)
		{
			return CreatedAtAction("Get", new { name = newUser.Name, lastname = newUser.Lastname, pageIndex = 0, count = 1 });
		}

		return BadRequest(response.OriginalException.Message);
    }

	[HttpGet]
	public async Task<ActionResult> Get([FromQuery]string name, string? lastname, int pageIndex, int count) {

		if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(lastname)) {
			return BadRequest("Search criteria is empty");
		}
		var searchResult = await _elasticSearch.SearchAsync(name, lastname, pageIndex, count);
        return Ok(searchResult);

    }
}
