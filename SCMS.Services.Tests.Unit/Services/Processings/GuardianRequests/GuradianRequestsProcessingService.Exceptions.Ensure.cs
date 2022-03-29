// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnEnsureIfValidationErrorOccursAndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();
            var someException = new Xeption();

            var expectedGuardianRequestProcessingDependencyValidationException =
                new GuardianRequestProcessingDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.guardianServiceMock.Setup(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExistsAsync(
                        someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingDependencyValidationException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestProcessingDependencyValidationException))),
                        Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionOnEnsureIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var expectedGuardianRequestProcessingDependencyException =
                new GuardianRequestProcessingDependencyException(
                    dependencyException.InnerException as Xeption);

            this.guardianServiceMock.Setup(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExistsAsync(
                        someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingDependencyException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestProcessingDependencyException))),
                        Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnEnsureIfServiceErrorOccursAndLogItAsync()
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();
            var serviceException = new Exception();

            var failedGuardianRequestProcessingException =
                new FailedGuardianRequestProcessingException(
                    serviceException);

            var expectedGuardianRequestProcessingServiceException =
                new GuardianRequestProcessingServiceException(
                    failedGuardianRequestProcessingException);

            this.guardianServiceMock.Setup(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExistsAsync(
                        someGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingServiceException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestProcessingServiceException))),
                        Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
