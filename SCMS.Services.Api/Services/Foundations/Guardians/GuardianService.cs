// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Guardians;
using System;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Foundations.Guardians
{
    public partial class GuardianService : IGuardianService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public GuardianService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Guardian> AddGuardianAsync(Guardian guardian) =>
        TryCatch(async () =>
        {
            ValidateGuardianOnAdd(guardian);

            return await this.storageBroker.InsertGuardianAsync(guardian);
        });

        public ValueTask<Guardian> RetrieveGuardianByIdAsync(Guid guardianId) =>
        TryCatch(async () =>
        {
            ValidateGuardianId(guardianId);

            return await this.storageBroker.SelectGuardianByIdAsync(guardianId);
        });
    }
}
