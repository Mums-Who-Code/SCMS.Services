// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionAddIfValidationErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var expectedStudentGuardianRequestOrchestrationDependencyValidationException =
                new StudentGuardianRequestOrchestrationDependencyValidationException(
                    dependencyException.InnerException as Xeption);

            this.studentProcessingServiceMock.Setup(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationDependencyValidationException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationDependencyValidationException))),
                        Times.Once);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyExceptions))]
        public async Task ShouldThrowDependencyExceptionAddIfDependencyErrorOccursAndLogItAsync(
            Xeption dependencyException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var expectedStudentGuardianRequestOrchestrationDependencyException =
                new StudentGuardianRequestOrchestrationDependencyException(
                    dependencyException.InnerException as Xeption);

            this.studentProcessingServiceMock.Setup(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationDependencyException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationDependencyException))),
                        Times.Once);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();
            var serviceException = new Exception();

            var failedStudentGuardianRequestOrchestrationException =
                new FailedStudentGuardianRequestOrchestrationException(
                    serviceException);

            var expectedStudentGuardianRequestOrchestrationServiceException =
                new StudentGuardianRequestOrchestrationServiceException(
                    failedStudentGuardianRequestOrchestrationException);

            this.studentProcessingServiceMock.Setup(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationServiceException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationServiceException))),
                        Times.Once);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(It.IsAny<GuardianRequest>()),
                    Times.Never);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
        }
    }
}
