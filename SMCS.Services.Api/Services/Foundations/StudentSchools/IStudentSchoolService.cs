// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.StudentSchools;

namespace SMCS.Services.Api.Services.Foundations.StudentSchools
{
    public interface IStudentSchoolService
    {
        ValueTask<StudentSchool> AddStudentSchool(StudentSchool studentSchool);
    }
}
