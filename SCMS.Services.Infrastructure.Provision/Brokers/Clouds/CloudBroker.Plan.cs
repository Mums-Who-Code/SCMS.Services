// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Threading.Tasks;
using OS = Microsoft.Azure.Management.AppService.Fluent.OperatingSystem;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial class CloudBroker
    {
        public async ValueTask<IAppServicePlan> CreatePlanAsync(
            string planName,
            IResourceGroup resourceGroup)
        {
            return await azure.AppServices.AppServicePlans
               .Define(planName)
               .WithRegion(region: Region.USWest2)
               .WithExistingResourceGroup(resourceGroup)
               .WithPricingTier(pricingTier: PricingTier.StandardS1)
               .WithOperatingSystem(operatingSystem: OS.Windows)
               .CreateAsync();
        }
    }
}
