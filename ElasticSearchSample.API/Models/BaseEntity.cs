using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSearchSample.API.Models;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
}