using Abp.Modules;
using Abp.Reflection.Extensions;
using CCB.Core.Shared;

namespace CCB.Core;

[DependsOn(typeof(CCBCoreSharedModule))]
public class CCBCoreModule: AbpModule
{
    public override void PreInitialize()
    {
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(CCBCoreModule).GetAssembly());
    }

    public override void PostInitialize()
    {
    }
}