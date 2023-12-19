using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CCB.Application.Shared.Common;
using CCB.Application.Shared.Dto.Common;
using CCB.Application.Shared.PostMenu.PostCategory.Dto;
using Microsoft.EntityFrameworkCore;
using Zero;

namespace CCB.Application.Common;

public class CCBAppService : ZeroAppServiceBase, ICCBAppService
{
    private readonly IRepository<Core.Post.PostCategory> _postCategoryRepository;

    public CCBAppService(IRepository<Core.Post.PostCategory> postCategoryRepository)
    {
        _postCategoryRepository = postCategoryRepository;
    }

    #region PostCategory

    private IQueryable<PostCategoryDto> PostCategoryQuery(CommonGetPostCategoryInput input)
    {
        var query = from obj in _postCategoryRepository.GetAll()
                .Where(o => !o.IsDeleted && o.IsActive && o.TenantId == AbpSession.TenantId)
                .WhereIf(input != null && !string.IsNullOrWhiteSpace(input.Filter),
                    e => e.Code.Contains(input.Filter) || e.Name.Contains(input.Filter))
                .WhereIf(input is { IsActive: not null }, o => o.IsActive == input.IsActive)
                .WhereIf(input is { Id: not null }, o => o.Id == input.Id)
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

    public async Task<List<PostCategoryDto>> GetPostCategories(CommonGetPostCategoryInput input)
    {
        var query = PostCategoryQuery(input);
        var res = await query.OrderBy(o => o.Code).ToListAsync();
        return res;
    }

    public async Task<PostCategoryDto> GetPostCategoryById(int id)
    {
        var query = PostCategoryQuery(new CommonGetPostCategoryInput { Id = id });
        var res = await query.FirstOrDefaultAsync();
        return res;
    }

    #endregion
}