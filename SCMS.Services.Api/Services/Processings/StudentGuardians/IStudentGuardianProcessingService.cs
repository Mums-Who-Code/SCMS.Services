// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public interface IStudentGuardianProcessingService
    {
        StudentGuardian VerifyNoPrimaryStudentGuardianExists(Guid studentId, Guid guardianId);
    }
}
