// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.GuardianRequests
{
    public partial class GuradianRequestsProcessingService
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnEnsureIfGuardianRequestIsNullAndLogItAsync()
        {
            // given
            GuardianRequest nullGuardianRequest = null;

            var nullGuardianRequestProcessingException =
                new NullGuardianRequestProcessingException();

            var expectedGuardianRequestValidationException =
                new GuardianRequestProcessingValidationException(
                    nullGuardianRequestProcessingException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExistsAsync(
                        nullGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingValidationException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestValidationException))),
                        Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnEnsureIfGuardianRequestIsInvalidAndLogItAsync()
        {
            // given
            var invalidGuardianRequest = new GuardianRequest();

            var invalidGuardianRequestProcessingException =
                new InvalidGuardianRequestProcessingException();

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.Id),
                values: "Id is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.FirstName),
                values: "Text is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.LastName),
                values: "Text is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.CountryCode),
                values: "Text is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.Occupation),
                values: "Text is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.CreatedDate),
                values: "Date is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.CreatedBy),
                values: "Id is required.");

            invalidGuardianRequestProcessingException.AddData(
                key: nameof(GuardianRequest.StudentId),
                values: "Id is required.");

            var expectedGuardianRequestProcessingValidationException =
                new GuardianRequestProcessingValidationException(
                    invalidGuardianRequestProcessingException);

            // when
            ValueTask<GuardianRequest> ensureGuardianRequestExistsTask =
                this.guardianRequestProcessingService
                    .EnsureGuardianRequestExistsAsync(
                        invalidGuardianRequest);

            // then
            await Assert.ThrowsAsync<GuardianRequestProcessingValidationException>(() =>
                ensureGuardianRequestExistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianRequestProcessingValidationException))),
                        Times.Once);

            this.guardianServiceMock.Verify(service =>
                service.RetrieveGuardianByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.guardianServiceMock.Verify(service =>
                service.AddGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.guardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
