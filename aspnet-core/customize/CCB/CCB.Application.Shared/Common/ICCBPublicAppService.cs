using CCB.Application.Shared.Common.Dto.Post;
using CCB.Application.Shared.PostMenu.Post.Dto;

namespace CCB.Application.Shared.Common;

public interface ICCBPublicAppService
{
    public Task<List<PostDto>> GetAllPosts(CommonGetPostInput input);
}