// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Agreements;

namespace SCMS.Services.Api.Services.Foundations.Agreements
{
    public class AgreementService : IAgreementService
    {
        private readonly IStorageBroker storageBroker;

        public AgreementService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Agreement> AddAgreementAsync(Agreement agreement) =>
            await this.storageBroker.InsertAgreementAsync(agreement);
    }
}
