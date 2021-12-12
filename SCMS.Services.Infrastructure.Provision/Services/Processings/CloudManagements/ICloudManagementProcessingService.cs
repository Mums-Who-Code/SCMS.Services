// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision.Services.Processings.CloudManagements
{
    internal interface ICloudManagementProcessingService
    {
        ValueTask ProcessAsync();
    }
}
