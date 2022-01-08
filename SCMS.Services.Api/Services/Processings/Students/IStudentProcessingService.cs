// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public interface IStudentProcessingService
    {
        ValueTask<Student> VerifyStudentExistsAsync(Guid studentId);
    }
}
