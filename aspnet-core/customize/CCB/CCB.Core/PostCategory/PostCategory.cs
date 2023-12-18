using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Zero.Customize.Base;

namespace CCB.Core.PostCategory;

[Table("CCB_Post_Category")]
public class PostCategory : SimpleFullAuditedEntity, IMayHaveTenant
{
    public int? TenantId { get; set; }
    public int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public virtual PostCategory? Parent { get; set; }
}