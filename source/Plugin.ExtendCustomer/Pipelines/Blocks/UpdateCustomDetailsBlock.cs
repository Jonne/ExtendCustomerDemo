using Plugin.ExtendCustomer.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Framework.Pipelines;

using System.Threading.Tasks;

namespace Plugin.ExtendCustomer.Pipelines.Blocks
{
    [PipelineDisplayName("MyPlugin:Blocks:UpdateCustomerDetailsBlock")]
    public class UpdateCustomDetailsBlock : Sitecore.Commerce.Plugin.Customers.UpdateCustomerDetailsBlock
    {
        public UpdateCustomDetailsBlock(IFindEntityPipeline findEntityPipeline) : base(findEntityPipeline)
        {
        }

        /// <summary>
        /// The default UpdateCustomerDetailsBlock only persists CustomerDetailsComponent.
        /// In addition this also persists MercuryCustomerDetailsComponent and PasswordResetComponent.
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<Customer> Run(Customer arg, CommercePipelineExecutionContext context)
        {
            var customer = await base.Run(arg, context);

            if (arg.HasComponent<CustomDetailsComponent>())
            {
                var customDetails = arg.GetComponent<CustomDetailsComponent>();
                customer.SetComponent(customDetails);
            }

            return customer;
        }
    }
}
