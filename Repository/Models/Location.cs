﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Location
{
    public int LocationId { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Street { get; set; }

    public virtual ICollection<BadmintonCourt> BadmintonCourts { get; set; } = new List<BadmintonCourt>();
}