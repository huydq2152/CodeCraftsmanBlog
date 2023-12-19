using AutoMapper;
using CCB.Application.Shared.Post.PostCategory.Dto;

namespace CCB.Application;

public class CustomDtoMapper
{
    public static void CreateMappings(IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<CreateOrEditPostCategoryDto, Core.Post.PostCategory>();
        configuration.CreateMap<PostCategoryDto, Core.Post.PostCategory>();
        configuration.CreateMap<PostCategoryDto, CreateOrEditPostCategoryDto>().ReverseMap();
    }
}