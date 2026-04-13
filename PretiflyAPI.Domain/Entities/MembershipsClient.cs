using System;
using System.Collections.Generic;

namespace PretiflyAPI.Domain.Entities;

public partial class MembershipsClient
{
    public int IdMembershipsClients { get; set; }

    public int IdClients { get; set; }

    public int IdMemebershipsTypes { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public DateTime? DateCancel { get; set; }

    public decimal ValuePay { get; set; }

    public virtual Client IdClientsNavigation { get; set; } = null!;

    public virtual MembershipsType IdMemebershipsTypesNavigation { get; set; } = null!;
}
