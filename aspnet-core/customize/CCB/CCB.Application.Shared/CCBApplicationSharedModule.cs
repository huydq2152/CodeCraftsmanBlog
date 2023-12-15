using Abp.Modules;
using Abp.Reflection.Extensions;
using CCB.Core.Shared;

namespace CCB.Application.Shared;

[DependsOn(typeof(CCBCoreSharedModule))]
public class CCBApplicationSharedModule: AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(CCBApplicationSharedModule).GetAssembly());
    }
}