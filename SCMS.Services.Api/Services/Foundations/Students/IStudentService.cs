// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> AddStudentAsync(Student student);

        ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId);
    }
}
