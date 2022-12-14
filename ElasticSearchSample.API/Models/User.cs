using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSearchSample.API.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Gender { get; set; }
    public DateTime Birthdate { get; set; }
    public string IP_Address { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ProfilePhoto { get; set; }
}
