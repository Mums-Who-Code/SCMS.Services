// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Brokers.DateTimes;
using SMCS.Services.Api.Brokers.Loggings;
using SMCS.Services.Api.Brokers.Storages;
using SMCS.Services.Api.Models.Foundations.Schools;

namespace SMCS.Services.Api.Services.Foundations.Schools
{
    public partial class SchoolService : ISchoolService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public SchoolService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<School> AddSchoolAsync(School school) =>
        TryCatch(async () =>
        {
            ValidateSchool(school);

            return await this.storageBroker.InsertSchoolAsync(school);
        });
    }
}
