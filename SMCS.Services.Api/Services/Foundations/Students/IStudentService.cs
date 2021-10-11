// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.Students;

namespace SMCS.Services.Api.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> AddStudentAsync(Student student);
    }
}
