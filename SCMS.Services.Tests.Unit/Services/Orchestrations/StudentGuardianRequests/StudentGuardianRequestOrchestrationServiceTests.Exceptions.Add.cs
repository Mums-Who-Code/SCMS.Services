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
            Xeption dependencyValidationException)
        {
            // given
            GuardianRequest someGuardianRequest = CreateRandomGuardianRequest();

            var expectedStudentGuardianRequestOrchestrationDependencyValidationException =
                new StudentGuardianRequestOrchestrationDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.studentProcessingServiceMock.Setup(service =>
                service.VerifyStudentExistsAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dependencyValidationException);

            // when
            ValueTask<GuardianRequest> addStudentGuardianRequestTask =
                this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(someGuardianRequest);

            // then
            await Assert.ThrowsAsync<StudentGuardianRequestOrchestrationDependencyValidationException>(() =>
                addStudentGuardianRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianRequestOrchestrationDependencyValidationException))),
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
