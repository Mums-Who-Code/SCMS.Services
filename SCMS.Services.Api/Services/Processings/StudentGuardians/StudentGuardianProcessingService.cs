﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public class StudentGuardianProcessingService : IStudentGuardianProcessingService
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

        public StudentGuardian VerifyPrimaryStudentGuardianExists(Guid studentId, Guid guardianId) =>
            throw new NotImplementedException();
    }
}
