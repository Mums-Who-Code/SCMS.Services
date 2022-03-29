// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using SCMS.Services.Infrastructure.Provision.Models.Storages;
using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision.Brokers.Clouds
{
    internal partial class CloudBroker
    {
        public async ValueTask<ISqlDatabase> CreateSqlDatabaseAsync(
            string sqlDatabaseName,
            ISqlServer sqlServer)
        {
            return await this.azure.SqlServers.Databases
                .Define(sqlDatabaseName)
                .WithExistingSqlServer(sqlServer)
                .CreateAsync();
        }

        public async ValueTask<ISqlServer> CreateSqlServerAsync(
            string sqlServerName,
            IResourceGroup resourceGroup)
        {
            return await this.azure.SqlServers.Define(sqlServerName)
                .WithRegion(region: Region.USWest2)
                .WithExistingResourceGroup(resourceGroup)
                .WithAdministratorLogin(administratorLogin: this.adminName)
                .WithAdministratorPassword(administratorLoginPassword: this.adminAccess)
                .CreateAsync();
        }

        public SqlDatabaseAccess GetAdminAccess()
        {
            return new SqlDatabaseAccess
            {
                AdminName = this.adminName,
                AdminAccess = this.adminAccess
            };
        }
    }
}
