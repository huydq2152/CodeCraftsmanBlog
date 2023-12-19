using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using CCB.Application.Shared.PostMenu.Post;
using CCB.Application.Shared.PostMenu.Post.Dto;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace CCB.Application.PostMenu.Post;

[AbpAuthorize(CCBPermissions.Post)]
public class PostAppService : ZeroAppServiceBase, IPostAppService
{
    private readonly IRepository<Core.Post.Post> _postRepository;

    public PostAppService(IRepository<Core.Post.Post> postRepository)
    {
        _postRepository = postRepository;
    }

    private IQueryable<PostDto> PostQuery(QueryInput queryInput)
    {
        var input = queryInput.Input;
        var id = queryInput.Id;

        var query = from obj in _postRepository.GetAll()
                .Where(o => !o.IsDeleted && o.TenantId == AbpSession.TenantId)
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
                .WhereIf(input is { IsActive: not null },
                    e => e.IsActive == input.IsActive)
                .WhereIf(id.HasValue, e => e.Id == id.Value)
            select new PostDto
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                Note = obj.Note,
                IsActive = obj.IsActive,
                Summary = obj.Summary,
                Content = obj.Content,
                Slug = obj.Slug,
                Url = obj.Url,

                PostCategoryId = obj.PostCategoryId,
                PostCategoryCode = obj.PostCategory.Code,
                PostCategoryName = obj.PostCategory.Name,

                IsDeleted = obj.IsDeleted,
                CreatorUserId = obj.CreatorUserId,
                CreationTime = obj.CreationTime,
                LastModifierUserId = obj.LastModifierUserId,
                LastModificationTime = obj.LastModificationTime,
            };
        return query;
    }

    private class QueryInput
    {
        public GetPostInput Input { get; init; }
        public int? Id { get; init; }
    }

    public async Task<PagedResultDto<GetPostForViewDto>> GetAll(GetPostInput input)
    {
        var queryInput = new QueryInput
        {
            Input = input
        };

        var objQuery = PostQuery(queryInput);
        var pagedAndFilteredAuthors = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

        var objs = from o in pagedAndFilteredAuthors
            select new GetPostForViewDto
            {
                Post = ObjectMapper.Map<PostDto>(o)
            };

        var totalCount = await objQuery.CountAsync();
        var res = await objs.ToListAsync();

        return new PagedResultDto<GetPostForViewDto>(
            totalCount,
            res
        );
    }

    [AbpAuthorize(CCBPermissions.Post_Edit)]
    public async Task<GetPostForEditOutput> GetPostForEdit(EntityDto input)
    {
        var queryInput = new QueryInput
        {
            Id = input.Id
        };

        var post = await PostQuery(queryInput).FirstOrDefaultAsync();
        var output = new GetPostForEditOutput
        {
            Post = ObjectMapper.Map<CreateOrEditPostDto>(post)
        };
        return output;
    }
    
    private async Task ValidateDataInput(CreateOrEditPostDto input)
    {
        var res = await _postRepository.GetAll().Where(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code.Equals(input.Code))
            .WhereIf(input.Id.HasValue, o => o.Id != input.Id).FirstOrDefaultAsync();
        if (res != null) throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
    }

    public async Task CreateOrEdit(CreateOrEditPostDto input)
    {
        input.Code = input.Code.Trim();
        await ValidateDataInput(input);
        if (!input.Id.HasValue)
            await Create(input);
        else
            await Update(input);
    }

    [AbpAuthorize(CCBPermissions.Post_Create)]
    private async Task Create(CreateOrEditPostDto input)
    {
        input.Url = $"{FrontPagePrefix.PostDetail}{input.Slug}".ToLower();
        var obj = ObjectMapper.Map<Core.Post.Post>(input);
        obj.TenantId = AbpSession.TenantId;
        await _postRepository.InsertAsync(obj);
    }

    [AbpAuthorize(CCBPermissions.Post_Edit)]
    private async Task Update(CreateOrEditPostDto input)
    {
        if (input.Id.HasValue)
        {
            var obj = await _postRepository.FirstOrDefaultAsync(o =>
                o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("Error"), L("EntityNotFound"));

            ObjectMapper.Map(input, obj);
            obj.TenantId = AbpSession.TenantId;

            await _postRepository.UpdateAsync(obj);
        }
    }

    public async Task Delete(EntityDto input)
    {
        var obj = await _postRepository.FirstOrDefaultAsync(
            o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
        if (obj == null) throw new UserFriendlyException(L("Error"), L("EntityNotFound"));
        await _postRepository.DeleteAsync(obj.Id);
    }
}