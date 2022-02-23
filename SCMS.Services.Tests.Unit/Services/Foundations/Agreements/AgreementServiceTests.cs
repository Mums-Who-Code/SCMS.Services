// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Services.Foundations.Agreements;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements
{
    public partial class AgreementServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IAgreementService agreementService;

        public AgreementServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.agreementService = new AgreementService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Agreement CreateRandomAgreement() =>
            CreateAgreementFiller(dateTime: GetRandomDateTime()).Create();

        private static Filler<Agreement> CreateAgreementFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Agreement>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime);

            return filler;
        }
    }
}