// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Services.Foundations.Students
{
    public partial class StudentService : IStudentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public StudentService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Student> AddStudentAsync(Student student) =>
        TryCatch(async () =>
        {
            ValidateStudentOnAdd(student);

            return await this.storageBroker.InsertStudentAsync(student);
        });

        public IQueryable<Student> RetrieveAllStudents()=>
           this.storageBroker.SelectAllStudents();

        public ValueTask<Student> RetrieveStudentByIdAsync(Guid studentId) =>
        TryCatch(async () =>
        {
            ValidateStudentId(studentId);

            return await this.storageBroker.SelectStudentByIdAsync(studentId);
        });
    }
}
