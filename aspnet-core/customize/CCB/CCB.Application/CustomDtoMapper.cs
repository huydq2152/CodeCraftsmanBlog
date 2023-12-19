using AutoMapper;
using CCB.Application.Shared.PostMenu.Post.Dto;
using CCB.Application.Shared.PostMenu.PostCategory.Dto;

namespace CCB.Application;

public class CustomDtoMapper
{
    public static void CreateMappings(IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<CreateOrEditPostCategoryDto, Core.Post.PostCategory>();
        configuration.CreateMap<PostCategoryDto, Core.Post.PostCategory>();
        configuration.CreateMap<PostCategoryDto, CreateOrEditPostCategoryDto>().ReverseMap();
        
        configuration.CreateMap<CreateOrEditPostDto, Core.Post.Post>();
        configuration.CreateMap<PostDto, Core.Post.Post>();
        configuration.CreateMap<PostDto, CreateOrEditPostDto>().ReverseMap();
    }
}