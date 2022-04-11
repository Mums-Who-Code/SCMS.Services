// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;

namespace SCMS.Services.Api.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionService : ITermsAndConditionService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public TermsAndConditionService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<TermsAndCondition> AddTermsAndConditionAsync(TermsAndCondition termsAndCondition) =>
        TryCatch(async () =>
        {
            ValidateInput(termsAndCondition);

            return await this.storageBroker.InsertTermsAndConditionAsync(termsAndCondition);
        });

    }
}
