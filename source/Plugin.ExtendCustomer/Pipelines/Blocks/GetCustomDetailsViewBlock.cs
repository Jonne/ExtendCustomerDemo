using Plugin.ExtendCustomer.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Framework.Pipelines;
using System.Threading.Tasks;

namespace Plugin.ExtendCustomer
{
    [PipelineDisplayName("MyPlugin:Blocks:GetCustomDetailsViewBlock")]
    public class GetCustomDetailsViewBlock : Sitecore.Commerce.Plugin.Customers.GetCustomerDetailsViewBlock
    {
        public GetCustomDetailsViewBlock(IGetLocalizedCustomerStatusPipeline getLocalizedCustomerStatusPipeline) : base(getLocalizedCustomerStatusPipeline)
        {
        }

        protected override async Task PopulateDetails(EntityView view, Customer customer, bool isAddAction, bool isEditAction, CommercePipelineExecutionContext context)
        {
            await base.PopulateDetails(view, customer, isAddAction, isEditAction, context);

            if (customer == null)
            {
                return;
            }

            var details = customer.GetComponent<CustomDetailsComponent>();

            view.Properties.Add(new ViewProperty
            {
                Name = nameof(CustomDetailsComponent.Title),
                IsRequired = false,
                RawValue = details?.Title,
                IsReadOnly = !isEditAction && !isAddAction
            });
        }
    }
}
