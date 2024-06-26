﻿using SmartSalon.Application.Domain.Base;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Application.Domain.Bookings;

public class Booking : DeletableEntity
{
    public string? Note { get; set; }
    public bool Done { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Id ServiceId { get; set; }
    public virtual Service? Service { get; set; }
    public Id CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
    public Id WorkerId { get; set; }
    public virtual Worker? Worker { get; set; }
}
