using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class Country
{
    public int IdCountries { get; set; }

    public string Countryname { get; set; } = null!;

    public string? CountryCode { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
