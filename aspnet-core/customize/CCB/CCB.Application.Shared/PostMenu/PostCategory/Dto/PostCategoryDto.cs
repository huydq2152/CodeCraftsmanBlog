using Zero.Customize.BaseDto;

namespace CCB.Application.Shared.PostMenu.PostCategory.Dto;

public class PostCategoryDto: SimpleFullAuditedEntityDto
{
    public int? TenantId { get; set; }
    public int? ParentId { get; set; }
    public string? ParentCode { get; set; }
    public string? ParentName { get; set; }
}