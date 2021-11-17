// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Theory]
        [MemberData(nameof(DependencyApiExceptions))]
        public void ShouldThrowDependencyExceptionOnVerifyIfDependencyErrorOccursndLogIt(
            Xeption dependencyApiException)
        {
            // given
            Guid someStudentId = Guid.NewGuid();
            Guid someGuardianId = Guid.NewGuid();

            var expectedStudentGuardianProcessingDependencyException =
                new StudentGuardianProcessingDependencyException(
                    dependencyApiException);

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Throws(dependencyApiException);

            // when
            Action verifyNoPrimaryStudentGuardianExistsTask = () =>
                this.studentGuardianProcessingService
                    .VerifyNoPrimaryStudentGuardianExists(
                        someStudentId,
                        someGuardianId);

            // then
            Assert.Throws<StudentGuardianProcessingDependencyException>(
                verifyNoPrimaryStudentGuardianExistsTask);

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingDependencyException))));

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
