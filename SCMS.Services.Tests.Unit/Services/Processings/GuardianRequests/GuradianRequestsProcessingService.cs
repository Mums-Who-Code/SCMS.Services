// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Services.Foundations.Guardians;
using SCMS.Services.Api.Services.Processings.GuardianRequests;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        private readonly Mock<IGuardianService> guardianServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianRequestProcessingService guardianRequestProcessingService;

        public GuradianRequestsProcessingService()
        {
            this.guardianServiceMock = new Mock<IGuardianService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.guardianRequestProcessingService = new GuardianRequestProcessingService(
                guardianService: this.guardianServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private Guardian CreateRandomGuardian() =>
            CreateGuardianFiller().Create();

        private Filler<Guardian> CreateGuardianFiller()
        {
            var filler = new Filler<Guardian>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler;
        }
    }
}
