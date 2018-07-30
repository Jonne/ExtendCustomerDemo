using Plugin.ExtendCustomer.Components;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.EntityViews;
using Sitecore.Commerce.Plugin.Customers;
using Sitecore.Framework.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.ExtendCustomer.Pipelines.Blocks
{
    [PipelineDisplayName("MyPlugin:Blocks:TranslateEntityViewToCustomer")]
    public class TranslateEntityViewToCustomerBlock : PipelineBlock<Customer, Customer, CommercePipelineExecutionContext>
    {
        public override Task<Customer> Run(Customer customer, CommercePipelineExecutionContext context)
        {
            if (customer == null || !customer.HasComponent<CustomerDetailsComponent>())
            {
                return Task.FromResult(customer);
            }

            var details = customer.GetComponent<CustomerDetailsComponent>();
            var customDetails = new CustomDetailsComponent();

            foreach (EntityView view in details.View.ChildViews)
            {
                foreach (ViewProperty viewProperty in view.Properties)
                {
                    if (viewProperty.Name == nameof(CustomDetailsComponent.Title))
                    {
                        customDetails.Title = view.GetPropertyValue(viewProperty.Name)?.ToString();
                    }
                }
            }

            customer.Components.Add(customDetails);

            return Task.FromResult(customer);
        }
    }
}
