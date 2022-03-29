// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Students;
using System;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public interface IStudentProcessingService
    {
        ValueTask<Student> VerifyStudentExistsAsync(Guid studentId);
    }
}
