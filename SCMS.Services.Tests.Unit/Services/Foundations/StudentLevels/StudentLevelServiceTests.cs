// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.StudentLevels;
using SCMS.Services.Api.Services.Foundations.StudentLevels;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentLevels
{
    public partial class StudentLevelServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentLevelService studentLevelService;

        public StudentLevelServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentLevelService = new StudentLevelService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
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

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static int GetRandomNumber() =>
           new IntRange(min: 2, max: 10).GetValue();

        private static int GetNegativeRandomNumber() =>
            -1 * GetRandomNumber();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentLevel CreateRandomStudentLevel(DateTimeOffset dates) =>
            CreateStudentLevelFiller(dates).Create();

        private static StudentLevel CreateRandomStudentLevel() =>
            CreateStudentLevelFiller(dates: GetRandomDateTime()).Create();

        private static Filler<StudentLevel> CreateStudentLevelFiller(DateTimeOffset dates)
        {
            var filler = new Filler<StudentLevel>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(studentLevel => studentLevel.CreatedByUser).IgnoreIt()
                .OnProperty(studentLevel => studentLevel.UpdatedByUser).IgnoreIt()
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
