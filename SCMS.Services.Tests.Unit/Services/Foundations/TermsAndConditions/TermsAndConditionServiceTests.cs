// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Services.Foundations.TermsAndConditions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITermsAndConditionService termsAndConditionService;

        public TermsAndConditionServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.termsAndConditionService = new TermsAndConditionService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData InvalidUrls()
        {
            string randomString = GetRandomString();
            string letterString = randomString;

            return new TheoryData<string>
            {
                null,
                "",
                "  ",
                letterString
            };
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

        private static int GetNegativeRandomNumber() =>
            -1 * GetRandomNumber();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static TermsAndConditionType GetRandomStatus()
        {
            TermsAndConditionType Type = TermsAndConditionType.Registration;

            return Type;
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

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 10).GetValue();

        private static TermsAndCondition CreateRandomTermsAndCondition() =>
            CreateTermsAndConditionFiller(dateTime: GetRandomDateTime()).Create();

        private static TermsAndCondition CreateRandomTermsAndCondition(DateTimeOffset dateTime) =>
           CreateTermsAndConditionFiller(dateTime).Create();

        private static Filler<TermsAndCondition> CreateTermsAndConditionFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<TermsAndCondition>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnType<Guid>().Use(userId)
                .OnProperty(termsAndCondition => termsAndCondition.Type).Use(TermsAndConditionType.Registration)
                .OnProperty(termsAndCondition => termsAndCondition.Url).Use(new RandomUrl())
                .OnProperty(termsAndCondition => termsAndCondition.CreatedByUser).IgnoreIt()
                .OnProperty(termsAndCondition => termsAndCondition.UpdatedByUser).IgnoreIt();

            return filler;
        }
    }
}
