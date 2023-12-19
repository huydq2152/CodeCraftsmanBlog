using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CCB.Application.Shared.PostMenu.PostCategory.Dto;

namespace CCB.Application.Shared.PostMenu.PostCategory;

public interface IPostCategoryAppService: IApplicationService
{
    Task<PagedResultDto<GetPostCategoryForViewDto>> GetAll(GetPostCategoryInput input);

    Task<GetPostCategoryForEditOutput> GetPostCategoryForEdit(EntityDto input);

    Task CreateOrEdit(CreateOrEditPostCategoryDto input);

    Task Delete(EntityDto input);
}