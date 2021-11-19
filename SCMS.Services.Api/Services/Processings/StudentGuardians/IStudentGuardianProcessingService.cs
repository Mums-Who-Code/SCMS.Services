// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public interface IStudentGuardianProcessingService
    {
        ValueTask<StudentGuardian> AddStudentGuardianAsync(StudentGuardian studentGuardian);
    }
}
