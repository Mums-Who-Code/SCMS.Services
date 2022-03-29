// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using SCMS.Services.Infrastructure.Provision.Models.Configurations;
using System.IO;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Configurations
{
    internal class ConfigurationBroker : IConfigurationBroker
    {
        public CloudManagementConfiguration GetConfigurations()
        {
            IConfigurationRoot configurations = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appSettings.json", optional: false)
                .Build();

            return configurations.Get<CloudManagementConfiguration>();
        }
    }
}
