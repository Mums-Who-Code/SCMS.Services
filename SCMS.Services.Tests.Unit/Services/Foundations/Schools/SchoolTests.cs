// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using SMCS.Services.Api.Brokers.DateTimes;
using SMCS.Services.Api.Brokers.Loggings;
using SMCS.Services.Api.Brokers.Storages;
using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Services.Foundations.Schools;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISchoolService schoolService;

        public SchoolTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.schoolService = new SchoolService(
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

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetRandomNegativeNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        private static School CreateRandomSchool() =>
            CreateRandomSchoolFiller(dates: GetRandomDateTimeOffset()).Create();

        private static School CreateRandomSchool(DateTimeOffset dates) =>
            CreateRandomSchoolFiller(dates).Create();

        private static Filler<School> CreateRandomSchoolFiller(DateTimeOffset dates)
        {
            var filler = new Filler<School>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
