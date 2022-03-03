// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Services.Foundations.Agreements;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements
{
    public partial class AgreementServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IAgreementService agreementService;
        private readonly Mock<ILoggingBroker> logginbrokerMock;
        public AgreementServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.logginbrokerMock = new Mock<ILoggingBroker>();

            this.agreementService = new AgreementService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.logginbrokerMock.Object);
        }
        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
                return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message;

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