using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Zero.ApiClient;
using Zero.Mobile.MAUI.Core.ApiClient;

namespace Zero
{
    [DependsOn(typeof(ZeroClientModule), typeof(AbpAutoMapperModule))]

    public class ZeroMobileMAUIModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.ReplaceService<IApplicationContext, MAUIApplicationContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ZeroMobileMAUIModule).GetAssembly());
        }
    }
}