// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Services.Foundations.TermsAndConditions;
using Tynamix.ObjectFiller;

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

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static TermsAndCondition CreateRandomTermsAndCondition() =>
            CreateTermsAndConditionFiller().Create();

        private static Filler<TermsAndCondition> CreateTermsAndConditionFiller()
        {
            var filler = new Filler<TermsAndCondition>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime())
                .OnProperty(termsAndCondition => termsAndCondition.CreatedByUser).IgnoreIt()
                .OnProperty(termsAndCondition => termsAndCondition.UpdatedByUser).IgnoreIt();

            return filler;
        }
    }
}
