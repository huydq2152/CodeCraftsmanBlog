using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CCB.Application.Shared;
using CCB.Core;

namespace CCB.Application;

/// <summary>
/// Application layer module of the application.
/// </summary>
[DependsOn(typeof(CCBApplicationSharedModule), typeof(CCBCoreModule))]
public class CCBApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(CCBApplicationModule).GetAssembly());
    }
}