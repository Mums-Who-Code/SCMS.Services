// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionAddIfStudentGuardianRequestIsNullAndLogItAsync()
        {
            // given
            GuardianRequest nullGuardianRequest = null;

            var nullStudentGuardianRequestOrchestrationException =
                new NullStudentGuardianRequestOrchestrationException();

            var expectedStudentGuardianRequestOrchestrationValidationException =
                new StudentGuardianRequestOrchestrationValidationException(
                    nullStudentGuardianRequestOrchestrationException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(nullGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationValidationException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationValidationException))),
                        Times.Once);

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionAddIfStudentGuardianRequestIsInvalidLogItAsync()
        {
            // given
            GuardianRequest invalidGuardianRequest = new GuardianRequest();

            var invalidStudentGuardianRequestOrchestrationException =
                new InvalidStudentGuardianRequestOrchestrationException();

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.Id),
                values: "Id is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.FirstName),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.LastName),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.EmailId),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.CountryCode),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.ContactNumber),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.Occupation),
                values: "Text is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.StudentId),
                values: "Id is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.CreatedDate),
                values: "Date is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.UpdatedDate),
                values: "Date is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.CreatedBy),
                values: "Id is required.");

            invalidStudentGuardianRequestOrchestrationException.AddData(
                key: nameof(GuardianRequest.UpdatedBy),
                values: "Id is required.");

            var expectedStudentGuardianRequestOrchestrationValidationException =
                new StudentGuardianRequestOrchestrationValidationException(
                    invalidStudentGuardianRequestOrchestrationException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(invalidGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationValidationException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationValidationException))),
                        Times.Once);

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
