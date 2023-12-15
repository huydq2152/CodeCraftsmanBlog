using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CCB.Core.Shared;

public class CCBCoreSharedModule: AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(CCBCoreSharedModule).GetAssembly());
    }
}