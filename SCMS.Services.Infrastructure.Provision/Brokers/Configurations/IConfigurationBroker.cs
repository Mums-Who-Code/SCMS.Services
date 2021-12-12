// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Infrastructure.Provision.Models.Configurations;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Configurations
{
    internal interface IConfigurationBroker
    {
        CloudManagementConfiguration GetConfigurations();
    }
}
