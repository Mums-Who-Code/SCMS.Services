// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public interface IStudentGuardianService
    {
        ValueTask<StudentGuardian> AddStudentGuardianAsync(StudentGuardian studentGuardian);
    }
}
