﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;

namespace Chonye.Domain.Models;

public partial class User
{
    public int UserId { get; set; }

    public int TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid? GlobalId { get; set; }

    public virtual ICollection<Datum> Data { get; set; } = new List<Datum>();

    public virtual Tenant Tenant { get; set; } = null!;
}