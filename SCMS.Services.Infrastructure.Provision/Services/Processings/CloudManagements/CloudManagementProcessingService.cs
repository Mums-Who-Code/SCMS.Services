// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using SCMS.Services.Infrastructure.Provision.Brokers.Configurations;
using SCMS.Services.Infrastructure.Provision.Models.Configurations;
using SCMS.Services.Infrastructure.Provision.Models.Storages;
using SCMS.Services.Infrastructure.Provision.Services.Foundations.CloudManagements;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SCMS.Services.Infrastructure.Provision.Services.Processings.CloudManagements
{
    internal class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly ICloudManagementService cloudManagementService;
        private readonly IConfigurationBroker configurationBroker;

        public CloudManagementProcessingService()
        {
            cloudManagementService = new CloudManagementService();
            this.configurationBroker = new ConfigurationBroker();
        }

        public async ValueTask ProcessAsync()
        {
            CloudManagementConfiguration cloudManagementConfiguration =
                this.configurationBroker.GetConfigurations();

            string projectName = cloudManagementConfiguration.ProjectName;
            await ProvisionAsync(projectName, cloudAction: cloudManagementConfiguration.Up);
            await DeprovisionAsync(projectName, cloudAction: cloudManagementConfiguration.Down);
        }

        private async ValueTask ProvisionAsync(string projectName, CloudAction cloudAction)
        {
            List<string> environments = RetrieveEnvironments(cloudAction);

            foreach (string environment in environments)
            {
                IResourceGroup resourceGroup = await this.cloudManagementService
                    .ProvisionResourceGroupAsync(
                        projectName,
                        environment);

                ISqlServer sqlServer = await this.cloudManagementService
                    .ProvisionSqlServerAsync(
                        projectName,
                        environment,
                        resourceGroup);

                SqlDatabase sqlDatabase = await this.cloudManagementService
                    .ProvisionSqlDatabaseAsync(
                        projectName,
                        environment,
                        sqlServer);

                IAppServicePlan appServicePlan = await this.cloudManagementService
                    .ProvisionPlanAsync(
                        projectName,
                        environment,
                        resourceGroup);

                IWebApp webApp = await this.cloudManagementService
                    .ProvisionWebAppAsync(
                        projectName,
                        environment,
                        databaseConnectionString: sqlDatabase.ConnectionString,
                        resourceGroup,
                        appServicePlan);
            }
        }

        private async ValueTask DeprovisionAsync(string projectName, CloudAction cloudAction)
        {
            List<string> environments = RetrieveEnvironments(cloudAction);

            foreach (string environment in environments)
            {
                await this.cloudManagementService.DeprovisionResourceGroupsAsync(
                    projectName,
                    environment);
            }
        }

        private static List<string> RetrieveEnvironments(CloudAction cloudAction) =>
            cloudAction?.Environments ?? new List<string>();
    }
}
