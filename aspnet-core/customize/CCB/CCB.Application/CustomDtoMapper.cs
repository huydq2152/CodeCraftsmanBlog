using AutoMapper;
using CCB.Application.Shared.Dto.PostCategory;
using CCB.Core.PostCategory;

namespace CCB.Application;

public class CustomDtoMapper
{
    public static void CreateMappings(IMapperConfigurationExpression configuration)
    {
        configuration.CreateMap<CreateOrEditPostCategoryDto, PostCategory>();
        configuration.CreateMap<PostCategoryDto, PostCategory>();
        configuration.CreateMap<PostCategoryDto, CreateOrEditPostCategoryDto>().ReverseMap();
    }
}