// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial class CloudBroker
    {
        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName)
        {
            return await azure.ResourceGroups
                .Define(resourceGroupName)
                .WithRegion(region: Region.USWest2)
                .CreateAsync();
        }

        public async ValueTask DeleteResourceGroupAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.DeleteByNameAsync(resourceGroupName);

        public async ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(resourceGroupName);
    }
}
