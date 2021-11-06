// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Services.Foundations.Guardians;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianService guardianService;

        public GuardianServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.guardianService = new GuardianService(
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

        private static T GetInvalidEnum<T>()
        {
            int randomNumber = GetLocalRandomNumber();

            while (Enum.IsDefined(typeof(T), randomNumber) is true)
            {
                randomNumber = GetLocalRandomNumber();
            }

            return (T)(object)randomNumber;

            static int GetLocalRandomNumber() =>
                new IntRange(min: int.MinValue, max: int.MaxValue)
                    .GetValue();
        }

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomMessage() =>
            new MnemonicString(wordCount: GetRandomNumber()).GetValue();

        public static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        public static int GetNegativeRandomNumber() =>
            -1 * GetRandomNumber();

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Guardian CreateRandomGuardian(DateTimeOffset date) =>
            CreateGuardianFiller(date).Create();

        private static Guardian CreateRandomGuardian() =>
            CreateGuardianFiller(date: GetRandomDateTime()).Create();

        private static Filler<Guardian> CreateGuardianFiller(DateTimeOffset date)
        {
            var filler = new Filler<Guardian>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
