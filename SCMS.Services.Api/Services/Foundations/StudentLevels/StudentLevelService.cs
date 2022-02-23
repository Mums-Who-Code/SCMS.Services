// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Services.Foundations.StudentLevels
{
    public partial class StudentLevelService : IStudentLevelService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentLevelService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<StudentLevel> AddStudentLevelAsync(StudentLevel studentLevel) =>
        TryCatch(async () =>
        {
            ValidateStudentLevel(studentLevel);

            return await this.storageBroker.InsertStudentLevelAsync(studentLevel);
        });
    }
}
