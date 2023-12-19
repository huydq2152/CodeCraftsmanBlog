using System.Linq.Dynamic.Core;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using CCB.Application.Shared.Dto.PostCategory;
using CCB.Application.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Zero;
using Zero.Authorization;

namespace CCB.Application.Service.PostCategory;

[AbpAuthorize(CCBPermissions.PostCategory)]
public class PostCategoryAppService : ZeroAppServiceBase, IPostCategoryAppService
{
    private readonly IRepository<Core.PostCategory.PostCategory> _postCategoryRepository;

    public PostCategoryAppService(IRepository<Core.PostCategory.PostCategory> postCategoryRepository)
    {
        _postCategoryRepository = postCategoryRepository;
    }

    private IQueryable<PostCategoryDto> PostCategoryQuery(QueryInput queryInput)
    {
        var input = queryInput.Input;
        var id = queryInput.Id;

        var query = from obj in _postCategoryRepository.GetAll()
                .Where(o => !o.IsDeleted && o.IsActive && o.TenantId == AbpSession.TenantId)
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
                .WhereIf(input is { IsActive: not null },
                    e => e.IsActive == input.IsActive)
                .WhereIf(id.HasValue, e => e.Id == id.Value)
            select new PostCategoryDto
            {
                Id = obj.Id,
                Code = obj.Code,
                Name = obj.Name,
                Note = obj.Note,
                IsActive = obj.IsActive,

                ParentId = obj.ParentId,
                ParentCode = obj.Parent.Code,
                ParentName = obj.Parent.Name,

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
        public GetPostCategoryInput Input { get; init; }
        public int? Id { get; init; }
    }

    public async Task<PagedResultDto<GetPostCategoryForViewDto>> GetAll(GetPostCategoryInput input)
    {
        var queryInput = new QueryInput
        {
            Input = input
        };

        var objQuery = PostCategoryQuery(queryInput);
        var pagedAndFilteredAuthors = objQuery.OrderBy(input.Sorting ?? "id asc").PageBy(input);

        var objs = from o in pagedAndFilteredAuthors
            select new GetPostCategoryForViewDto
            {
                PostCategory = ObjectMapper.Map<PostCategoryDto>(o)
            };

        var totalCount = await objQuery.CountAsync();
        var res = await objs.ToListAsync();

        return new PagedResultDto<GetPostCategoryForViewDto>(
            totalCount,
            res
        );
    }

    [AbpAuthorize(CCBPermissions.PostCategory_Edit)]
    public async Task<GetPostCategoryForEditOutput> GetPostCategoryForEdit(EntityDto input)
    {
        var queryInput = new QueryInput
        {
            Id = input.Id
        };

        var postCategory = await PostCategoryQuery(queryInput).FirstOrDefaultAsync();
        var output = new GetPostCategoryForEditOutput
        {
            PostCategory = ObjectMapper.Map<CreateOrEditPostCategoryDto>(postCategory)
        };
        return output;
    }

    private async Task ValidateDataInput(CreateOrEditPostCategoryDto input)
    {
        var res = await _postCategoryRepository.GetAll().Where(o =>
                !o.IsDeleted && o.TenantId == AbpSession.TenantId && o.Code.Equals(input.Code))
            .WhereIf(input.Id.HasValue, o => o.Id != input.Id).FirstOrDefaultAsync();
        if (res != null) throw new UserFriendlyException(L("Error"), L("CodeAlreadyExists"));
    }

    public async Task CreateOrEdit(CreateOrEditPostCategoryDto input)
    {
        input.Code = input.Code.Trim();
        await ValidateDataInput(input);
        if (!input.Id.HasValue)
            await Create(input);
        else
            await Update(input);
    }

    [AbpAuthorize(CCBPermissions.PostCategory_Create)]
    private async Task Create(CreateOrEditPostCategoryDto input)
    {
        var obj = ObjectMapper.Map<Core.PostCategory.PostCategory>(input);
        obj.TenantId = AbpSession.TenantId;
        await _postCategoryRepository.InsertAsync(obj);
    }

    [AbpAuthorize(CCBPermissions.PostCategory_Edit)]
    private async Task Update(CreateOrEditPostCategoryDto input)
    {
        if (input.Id.HasValue)
        {
            var obj = await _postCategoryRepository.FirstOrDefaultAsync(o =>
                o.TenantId == AbpSession.TenantId && o.Id == input.Id);
            if (obj == null) throw new UserFriendlyException(L("Error"), L("EntityNotFound"));

            ObjectMapper.Map(input, obj);
            obj.TenantId = AbpSession.TenantId;

            await _postCategoryRepository.UpdateAsync(obj);
        }
    }

    public async Task Delete(EntityDto input)
    {
        var obj = _postCategoryRepository.FirstOrDefaultAsync(
            o => o.TenantId == AbpSession.TenantId && o.Id == input.Id);
        if (obj == null) throw new UserFriendlyException(L("Error"), L("EntityNotFound"));
        await _postCategoryRepository.DeleteAsync(obj.Id);
    }
}