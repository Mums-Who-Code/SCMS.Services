// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial interface ICloudBroker
    {
        ValueTask<IAppServicePlan> CreatePlanAsync(string planName, IResourceGroup resourceGroup);
    }
}
