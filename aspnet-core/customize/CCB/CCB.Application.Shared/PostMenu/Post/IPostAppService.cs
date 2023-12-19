using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CCB.Application.Shared.PostMenu.Post.Dto;

namespace CCB.Application.Shared.PostMenu.Post;

public interface IPostAppService: IApplicationService
{
    Task<PagedResultDto<GetPostForViewDto>> GetAll(GetPostInput input);

    Task<GetPostForEditOutput> GetPostForEdit(EntityDto input);

    Task CreateOrEdit(CreateOrEditPostDto input);

    Task Delete(EntityDto input);
}