// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Infrastructure.Provision.Services.Processings.CloudManagements;
using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ICloudManagementProcessingService cloudManagementProcessingService =
                new CloudManagementProcessingService();

            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}
