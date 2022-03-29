// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using System.Linq;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public interface IStudentGuardianService
    {
        ValueTask<StudentGuardian> AddStudentGuardianAsync(StudentGuardian studentGuardian);
        IQueryable<StudentGuardian> RetrieveAllStudentGuardians();
    }
}
