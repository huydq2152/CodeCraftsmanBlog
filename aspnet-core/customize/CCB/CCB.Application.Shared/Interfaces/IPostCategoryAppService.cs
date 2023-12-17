using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CCB.Application.Shared.Dto.PostCategory;

namespace CCB.Application.Shared.Interfaces;

public interface IPostCategoryAppService: IApplicationService
{
    Task<PagedResultDto<GetPostCategoryForViewDto>> GetAll(GetPostCategoryInput input);

    Task<GetPostCategoryForEditOutput> GetPostCategoryForEdit(EntityDto input);

    Task CreateOrEdit(CreateOrEditPostCategoryDto input);

    Task Delete(EntityDto input);
}