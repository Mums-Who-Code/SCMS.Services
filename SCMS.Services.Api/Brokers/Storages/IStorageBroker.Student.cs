// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<Student> InsertStudentAsync(Student student);
        public IQueryable<Student> SelectAllStudents();
        public ValueTask<Student> SelectStudentByIdAsync(Guid studentId);
    }
}
