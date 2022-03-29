// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public interface IStudentGuardianProcessingService
    {
        ValueTask<StudentGuardian> AddStudentGuardianAsync(StudentGuardian studentGuardian);
    }
}
