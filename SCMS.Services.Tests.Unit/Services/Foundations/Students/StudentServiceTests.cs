﻿// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Services.Foundations.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentService studentService;

        public StudentServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentService = new StudentService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        public static IEnumerable<object[]> InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new List<object[]>
            {
                new object[] { randomMoreThanMinuteFromNow },
                new object[] { randomMoreThanMinuteBeforeNow }
            };
        }

        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static SqlException GetSqlException() =>
           (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static Student CreateRandomStudent(DateTimeOffset dateTime) =>
            CreateStudentFiller(dateTime).Create();

        private static Student CreateRandomStudent() =>
            CreateStudentFiller(dateTime: GetRandomDateTime()).Create();

        private static IQueryable<Student> CreateRandomStudents()
        {
            return CreateStudentFiller(dateTime: GetRandomDateTime())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static Filler<Student> CreateStudentFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Student>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnProperty(student => student.Status).Use(StudentStatus.Active)
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnType<Guid>().Use(userId)
                .OnProperty(student => student.CreatedByUser).IgnoreIt()
                .OnProperty(student => student.UpdatedByUser).IgnoreIt()
                .OnProperty(student => student.RegisteredGuardians).IgnoreIt()
                .OnProperty(student => student.EnrolledSchool).IgnoreIt();

            return filler;
        }
    }
}
