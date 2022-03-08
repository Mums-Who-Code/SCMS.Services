// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Services.Foundations.TermsAndConditions;
using Tynamix.ObjectFiller;
using Xeptions;

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

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static TermsAndCondition CreateRandomTermsAndCondition() =>
            CreateTermsAndConditionFiller().Create();

        private static Filler<TermsAndCondition> CreateTermsAndConditionFiller()
        {
            var filler = new Filler<TermsAndCondition>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime())
                .OnProperty(termsAndCondition => termsAndCondition.Type).Use(GetRandomStatus())
                .OnProperty(termsAndCondition => termsAndCondition.CreatedByUser).IgnoreIt()
                .OnProperty(termsAndCondition => termsAndCondition.UpdatedByUser).IgnoreIt();

            return filler;
        }
    }
}
