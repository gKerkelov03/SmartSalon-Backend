﻿

namespace SmartSalon.Application.Domain.Abstractions;

public interface IDeletableEntity
{
    public DateTime? DeletedOn { get; set; }

    public Id? DeletedBy { get; set; }

    public bool IsDeleted { get; set; }
}
