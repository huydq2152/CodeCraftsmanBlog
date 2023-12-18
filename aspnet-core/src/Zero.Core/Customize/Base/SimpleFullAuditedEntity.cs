using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace Zero.Customize.Base;

public class SimpleFullAuditedEntity : FullAuditedEntity
{
    [StringLength(ZeroConst.MaxCodeLength)]
    public string Code { get; set; }
    [Required]
    [StringLength(ZeroConst.MaxNameLength, MinimumLength = ZeroConst.MinNameLength)]
    public string Name { get; set; }
    public string Note { get; set; }
    public bool IsActive { get; set; }
}