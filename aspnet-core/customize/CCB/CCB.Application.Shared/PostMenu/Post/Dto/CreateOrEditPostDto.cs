using Zero.Customize.BaseDto;

namespace CCB.Application.Shared.PostMenu.Post.Dto;

public class CreateOrEditPostDto: SimpleFullAuditedCreateOrEditEntityDto
{
    public int? TenantId { get; set; }
    public int PostCategoryId { get; set; }
    public string? PostCategoryCode { get; set; }
    public string? PostCategoryName { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public string Slug { get; set; }
    public string? Url { get; set; }
}