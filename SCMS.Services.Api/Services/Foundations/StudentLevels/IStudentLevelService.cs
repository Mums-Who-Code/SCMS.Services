// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentLevels;

namespace SCMS.Services.Api.Services.Foundations.StudentLevels
{
    public interface IStudentLevelService
    {
        ValueTask<StudentLevel> AddStudentLevelAsync(StudentLevel studentLevel);
    }
}
