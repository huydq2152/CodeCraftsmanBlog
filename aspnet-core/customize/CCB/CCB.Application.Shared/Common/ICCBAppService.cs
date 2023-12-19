using CCB.Application.Shared.Dto.Common;
using CCB.Application.Shared.PostMenu.PostCategory.Dto;

namespace CCB.Application.Shared.Common;

public interface ICCBAppService
{
    public Task<List<PostCategoryDto>> GetPostCategories(CommonGetPostCategoryInput input);
    public Task<PostCategoryDto> GetPostCategoryById(int id);
}