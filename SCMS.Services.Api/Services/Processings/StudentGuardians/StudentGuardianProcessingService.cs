// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;

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

        public StudentGuardian VerifyNoPrimaryStudentGuardianExists(Guid studentId, Guid guardianId) =>
        TryCatch(() =>
        {
            ValidateIds(studentId, guardianId);
            StudentGuardian retrievedStudentGuardian = RetrievePrimaryStudentGuardian(studentId, guardianId);
            ValidateStudentGuardian(retrievedStudentGuardian);

            return retrievedStudentGuardian;
        });

        private StudentGuardian RetrievePrimaryStudentGuardian(Guid studentId, Guid guardianId)
        {
            IQueryable<StudentGuardian> retrievedStudentGuardians =
                            this.studentGuardianService.RetrieveAllStudentGuardians();

            return retrievedStudentGuardians
                .Where(studentGuardian => studentGuardian.GuardianId == guardianId
                    && studentGuardian.StudentId == studentId
                    && studentGuardian.Level == ContactLevel.Primary)
                        .FirstOrDefault();
        }
    }
}
