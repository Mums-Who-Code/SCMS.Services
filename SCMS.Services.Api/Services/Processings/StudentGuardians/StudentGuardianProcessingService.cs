// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService : IStudentGuardianProcessingService
    {
        private readonly IStudentGuardianService studentGuardianService;
        private readonly ILoggingBroker loggingBroker;

        public StudentGuardianProcessingService(
            IStudentGuardianService studentGuardianService,
            ILoggingBroker loggingBroker)
        {
            this.studentGuardianService = studentGuardianService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<StudentGuardian> AddStudentGuardianAsync(StudentGuardian studentGuardian) =>
        TryCatch(async () =>
        {
            ValidateStudentGuardian(studentGuardian);

            return await this.studentGuardianService.AddStudentGuardianAsync(studentGuardian);
        });
    }
}
