﻿using SmartSalon.Application.Domain.Abstractions;

namespace SmartSalon.Application.Domain;

public class SpecialSlot : BaseEntity
{
    public Id BookingTimeId { get; set; }

    public virtual BookingTime? BookingTime { get; set; }

    public virtual ICollection<Subscription>? Subscription { get; set; }
}