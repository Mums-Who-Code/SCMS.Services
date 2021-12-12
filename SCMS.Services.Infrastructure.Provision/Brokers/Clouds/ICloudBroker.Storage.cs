// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using SCMS.Services.Infrastructure.Provision.Models.Storages;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial interface ICloudBroker
    {
        ValueTask<ISqlServer> CreateSqlServerAsync(string sqlServerName, IResourceGroup resourceGroup);
        ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(string sqlDatabaseName, ISqlServer sqlServer);
        SqlDatabaseAccess GetAdminAccess();
    }
}
