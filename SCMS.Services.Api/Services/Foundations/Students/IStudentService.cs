// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Students;
using System;
using System.Threading.Tasks;

namespace SCMS.Services.Api.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> AddStudentAsync(Student student);

        ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId);
    }
}
