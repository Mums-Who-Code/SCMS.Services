﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Services.Foundations.Students;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public class StudentProcessingService : IStudentProcessingService
    {
        private readonly IStudentService studentService;
        private readonly ILoggingBroker loggingBroker;

        public StudentProcessingService(
            IStudentService studentService,
            ILoggingBroker loggingBroker)
        {
            this.studentService = studentService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Student> VerifyStudentExistsAsync(Guid studentId) =>
            this.studentService.RetrieveStudentByIdAsync(studentId);
    }
}