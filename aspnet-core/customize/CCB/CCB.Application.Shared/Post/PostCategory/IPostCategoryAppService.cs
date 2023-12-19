using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CCB.Application.Shared.Post.PostCategory.Dto;

namespace CCB.Application.Shared.Post.PostCategory;

public interface IPostCategoryAppService: IApplicationService
{
    Task<PagedResultDto<GetPostCategoryForViewDto>> GetAll(GetPostCategoryInput input);

    Task<GetPostCategoryForEditOutput> GetPostCategoryForEdit(EntityDto input);

    Task CreateOrEdit(CreateOrEditPostCategoryDto input);

    Task Delete(EntityDto input);
}