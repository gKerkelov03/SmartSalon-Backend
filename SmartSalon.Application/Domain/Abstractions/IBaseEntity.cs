
using System.ComponentModel.DataAnnotations;

namespace SmartSalon.Application.Domain.Abstractions;

public interface IBaseEntity<TKey>
{
    [Key]
    public TKey Id { get; set; }
}
