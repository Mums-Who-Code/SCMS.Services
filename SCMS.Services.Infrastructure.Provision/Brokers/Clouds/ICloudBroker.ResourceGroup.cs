// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Azure.Management.ResourceManager.Fluent;
using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial interface ICloudBroker
    {
        ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName);
        ValueTask DeleteResourceGroupAsync(string resourceGroupName);
        ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName);
    }
}
