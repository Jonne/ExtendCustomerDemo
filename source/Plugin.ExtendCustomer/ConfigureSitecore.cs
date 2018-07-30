namespace Plugin.ExtendCustomer
{
    using System.Reflection;

    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Commerce.Core;
    using Sitecore.Commerce.EntityViews;
    using Sitecore.Commerce.Plugin.Customers;
    using Sitecore.Framework.Configuration;
    using Sitecore.Framework.Pipelines.Definitions.Extensions;

    /// <summary>
    /// The Habitat configure class.
    /// </summary>
    /// <seealso cref="IConfigureSitecore" />
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(
                config =>
                    config
                .ConfigurePipeline<IGetEntityViewPipeline>(c =>
                {
                    c.Replace<GetCustomerDetailsViewBlock, GetCustomDetailsViewBlock>();
                })
                .ConfigurePipeline<ITranslateEntityViewToCustomerPipeline>(c =>
                {
                    c.Add<Pipelines.Blocks.TranslateEntityViewToCustomerBlock>()
                     .After<TranslateEntityViewToCustomerBlock>();
                })
                .ConfigurePipeline<IUpdateCustomerDetailsPipeline>(c =>
                {
                    c.Replace<UpdateCustomerDetailsBlock,
                       Pipelines.Blocks.UpdateCustomDetailsBlock>();
                })
                        .ConfigurePipeline<IRunningPluginsPipeline>(c =>
                        {
                            c.Add<RegisteredPluginBlock>().After<RunningPluginsBlock>();
                        }));
        }
    }
}