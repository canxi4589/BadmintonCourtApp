﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Role { get; set; }

    public string Name { get; set; }

    public string Gmail { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}