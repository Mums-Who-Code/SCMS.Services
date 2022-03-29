// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using KellermanSoftware.CompareNetObjects;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Guardians.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Foundations.Guardians;
using SCMS.Services.Api.Services.Processings.GuardianRequests;
using System;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        private readonly Mock<IGuardianService> guardianServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IGuardianRequestProcessingService guardianRequestProcessingService;
        private readonly ICompareLogic compareLogic;

        public GuradianRequestsProcessingService()
        {
            this.guardianServiceMock = new Mock<IGuardianService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.guardianRequestProcessingService = new GuardianRequestProcessingService(
                guardianService: this.guardianServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyValidationExceptions()
        {
            var someXeption = new Xeption();

            return new TheoryData<Xeption>
            {
                new GuardianValidationException(someXeption),
                new GuardianDependencyValidationException(someXeption)
            };
        }

        public static TheoryData DependencyExceptions()
        {
            var someXeption = new Xeption();

            return new TheoryData<Xeption>
            {
                new GuardianDependencyException(someXeption),
                new GuardianServiceException(someXeption)
            };
        }

        private DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private Expression<Func<Guardian, bool>> SameGuardianAs(
            Guardian expectedGuardian)
        {
            return actualGuardian =>
                this.compareLogic.Compare(expectedGuardian, actualGuardian)
                    .AreEqual;
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private Guardian CreateRandomGuardian() =>
            CreateGuardianFiller().Create();

        private GuardianRequest CreateRandomGuardianRequest() =>
            CreateGuardianRequestFiller().Create();

        private Filler<Guardian> CreateGuardianFiller()
        {
            var filler = new Filler<Guardian>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime())
                .OnProperty(guardian => guardian.CreatedByUser).IgnoreIt()
                .OnProperty(guardian => guardian.UpdatedByUser).IgnoreIt()
                .OnProperty(guardian => guardian.RegisteredStudents).IgnoreIt();

            return filler;
        }

        private Filler<GuardianRequest> CreateGuardianRequestFiller()
        {
            var filler = new Filler<GuardianRequest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler;
        }
    }
}
