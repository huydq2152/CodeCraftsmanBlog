using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Zero.Customize.Base;

namespace CCB.Core.PostCategory;

[Table("CCB_Blog_Category")]
public class PostCategory : SimpleFullAuditedEntity, IMayHaveTenant
{
    public int? TenantId { get; set; }
    
    public int? ParentId { get; set; }
    [ForeignKey("ParentId")]
    public virtual PostCategory? Parent { get; set; }
}