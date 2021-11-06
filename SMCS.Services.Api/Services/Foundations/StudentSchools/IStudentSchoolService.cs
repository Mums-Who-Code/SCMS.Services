// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentSchools;

namespace SCMS.Services.Api.Services.Foundations.StudentSchools
{
    public interface IStudentSchoolService
    {
        ValueTask<StudentSchool> AddStudentSchoolAsync(StudentSchool studentSchool);
    }
}
