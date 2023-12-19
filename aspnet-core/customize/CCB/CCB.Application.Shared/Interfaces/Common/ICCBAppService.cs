using CCB.Application.Shared.Dto.Common;
using CCB.Application.Shared.Dto.PostCategory;

namespace CCB.Application.Shared.Interfaces.Common;

public interface ICCBAppService
{
    public Task<List<PostCategoryDto>> GetPostCategories(CommonGetPostCategoryInput input);
    public Task<PostCategoryDto> GetPostCategoryById(int id);
}