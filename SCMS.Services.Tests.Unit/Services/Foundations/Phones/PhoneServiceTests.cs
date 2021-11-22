// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Services.Foundations.Phones;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Phones
{
    public partial class PhoneServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPhoneService phoneService;

        public PhoneServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.phoneService = new PhoneService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Phone CreateRandomPhone() =>
            CreatePhoneFiller(dates: GetRandomDateTime()).Create();

        private static Filler<Phone> CreatePhoneFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Phone>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<Guid>().Use(userId)
                .OnProperty(studentGuardian => studentGuardian.CreatedByUser).IgnoreIt()
                .OnProperty(studentGuardian => studentGuardian.UpdatedByUser).IgnoreIt()
                .OnProperty(studentGuardian => studentGuardian.Guardian).IgnoreIt();

            return filler;
        }
    }
}
