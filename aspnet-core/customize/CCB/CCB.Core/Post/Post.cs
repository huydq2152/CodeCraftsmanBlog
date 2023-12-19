using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Zero.Customize.Base;

namespace CCB.Core.Post;

[Table("CCB_Post_Post")]
public class Post: SimpleFullAuditedEntity, IMayHaveTenant
{
    public int? TenantId { get; set; }
    
    public int PostCategoryId { get; set; }
    [ForeignKey("PostCategoryId")]
    public virtual PostCategory? PostCategory { get; set; }
    
    public string Summary { get; set; }
    public string Content { get; set; }
    public string Slug { get; set; }
    public string Url { get; set; }
}