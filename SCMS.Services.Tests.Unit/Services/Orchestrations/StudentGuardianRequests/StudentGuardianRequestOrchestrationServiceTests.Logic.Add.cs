// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentGuardianRequestAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            StudentGuardian returningStudentGuardian = inputStudentGuardian;

            GuardianRequest randomGuardianRequest =
                CreateRandomGuardianRequestWith(randomStudentGuardian);

            GuardianRequest inputGuardianRequest = randomGuardianRequest;
            GuardianRequest returningGuardianRequest = inputGuardianRequest;
            GuardianRequest expectedGuardianRequest = inputGuardianRequest.DeepClone();
            Guid inputStudentId = inputGuardianRequest.StudentId;
            var mockSequence = new MockSequence();

            this.studentProcessingServiceMock.InSequence(mockSequence).Setup(service =>
                service.VerifyStudentExistsAsync(inputStudentId));

            this.guardianRequestProcessingServiceMock.InSequence(mockSequence).Setup(service =>
                service.EnsureGuardianRequestExists(inputGuardianRequest))
                    .ReturnsAsync(returningGuardianRequest);

            this.studentGuardianProcessingServiceMock.InSequence(mockSequence).Setup(service =>
                service.AddStudentGuardianAsync(It.Is(SameStudentGuardianAs(inputStudentGuardian))))
                    .ReturnsAsync(returningStudentGuardian);

            // when
            GuardianRequest actualGuardianRequest =
                await this.studentGuardianRequestOrchestrationService
                    .AddStudentGuardianRequestAsync(inputGuardianRequest);

            // then
            actualGuardianRequest.Should().BeEquivalentTo(expectedGuardianRequest);

            this.studentProcessingServiceMock.Verify(service =>
                service.VerifyStudentExistsAsync(inputStudentId),
                    Times.Once);

            this.guardianRequestProcessingServiceMock.Verify(service =>
                service.EnsureGuardianRequestExists(inputGuardianRequest),
                    Times.Once);

            this.studentGuardianProcessingServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.Is(SameStudentGuardianAs(
                    inputStudentGuardian))),
                        Times.Once);

            this.studentProcessingServiceMock.VerifyNoOtherCalls();
            this.guardianRequestProcessingServiceMock.VerifyNoOtherCalls();
            this.studentGuardianProcessingServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
