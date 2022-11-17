using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSearchSample.API.Models;

public class BaseEntity
{

    [Column(name: "id")]
    public Guid Id { get; set; }
}