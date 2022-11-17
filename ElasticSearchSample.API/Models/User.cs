using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticSearchSample.API.Models;

public class User : BaseEntity
{
    
    [Column(name: "name")]
    public string Name { get; set; }

    [Column(name: "lastname")]
    public string Lastname { get; set; }
    
    [Column(name: "email")]
    public string Email { get; set; }

    [Column(name: "title")]
    public string Title { get; set; }

    [Column(name: "gender")]
    public string Gender { get; set; }

    [Column(name: "birthdate")]
    public DateTime Birthdate { get; set; }
    
    [Column(name: "ip_address")]
    public string IP_Address { get; set; }

    [Column(name: "city")]
    public string City { get; set; }

    [Column(name: "address")]
    public string Address { get; set; }

    [Column(name: "profile_photo")]
    public string ProfilePhoto { get; set; }


}
