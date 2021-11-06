// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.StudentSchools;
using SCMS.Services.Api.Services.Foundations.StudentSchools;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentSchoolService studentSchoolService;

        public StudentSchoolTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentSchoolService = new StudentSchoolService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData InvalidMinuteCases()
        {
            int minutesInFuture = GetRandomNumber();
            int minutesInPast = GetRandomNegativeNumber();

            return new TheoryData<int>
            {
                minutesInFuture,
                minutesInPast
            };
        }

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentSchool CreateRandomStudentSchool() =>
            CreateStudentSchoolFiller(dates: GetRandomDateTime()).Create();

        private static StudentSchool CreateRandomStudentSchool(DateTimeOffset dates) =>
            CreateStudentSchoolFiller(dates).Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetRandomNegativeNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Filler<StudentSchool> CreateStudentSchoolFiller(DateTimeOffset dates)
        {
            var filler = new Filler<StudentSchool>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
