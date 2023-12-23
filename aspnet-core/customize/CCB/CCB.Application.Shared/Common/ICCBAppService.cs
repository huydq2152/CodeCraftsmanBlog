using CCB.Application.Shared.Common.Dto.PostCategory;
using CCB.Application.Shared.PostMenu.PostCategory.Dto;

namespace CCB.Application.Shared.Common;

public interface ICCBAppService
{
    public Task<List<PostCategoryDto>> GetPostCategories(CommonGetPostCategoryInput input);
    public Task<PostCategoryDto> GetPostCategoryById(int id);
}