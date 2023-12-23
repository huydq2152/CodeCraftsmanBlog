using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CCB.Application.Shared.Common;
using CCB.Application.Shared.Common.Dto.Post;
using CCB.Application.Shared.PostMenu.Post.Dto;
using CCB.Core.Post;
using Microsoft.EntityFrameworkCore;
using Zero;

namespace CCB.Application.Common;

public class CCBPublicAppService : ZeroAppServiceBase, ICCBPublicAppService
{
    private readonly IRepository<Post> _postRepository;

    public CCBPublicAppService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    #region Post
    
    private IQueryable<PostDto> PostQuery(CommonGetPostInput input)
    {
        var query = from obj in _postRepository.GetAll()
                .Where(o => !o.IsDeleted && o.IsActive && o.TenantId == AbpSession.TenantId)
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
                .WhereIf(input is { IsActive: not null }, o => o.IsActive == input.IsActive)
            select new PostDto
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                Note = obj.Note,
                IsActive = obj.IsActive,
                
                PostCategoryId = obj.PostCategoryId,
                PostCategoryCode = obj.PostCategory.Code,
                PostCategoryName = obj.PostCategory.Name,
                
                Summary = obj.Summary,
                Content = obj.Content,
                Slug = obj.Slug,
                Url = obj.Url,

                IsDeleted = obj.IsDeleted,
                CreatorUserId = obj.CreatorUserId,
                CreationTime = obj.CreationTime,
                LastModifierUserId = obj.LastModifierUserId,
                LastModificationTime = obj.LastModificationTime,
            };
        return query;
    }

    public async Task<List<PostDto>> GetAllPosts(CommonGetPostInput input)
    {
        var posts = PostQuery(input);
        var res = await posts.OrderBy(o => o.Code).ToListAsync();
        return res;
    }

    #endregion
    
}