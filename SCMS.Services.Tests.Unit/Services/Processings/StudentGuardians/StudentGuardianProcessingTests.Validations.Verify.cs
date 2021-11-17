// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnVerifyIfIdsIsInvalidAndLogIt()
        {
            // given
            Guid invalidStudentId = Guid.Empty;
            Guid invalidGuardianId = Guid.Empty;

            var invalidStudentGuardianException = new
                InvalidStudentGuardianProcessingException();

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.StudentId),
                values: "Id is required");

            invalidStudentGuardianException.AddData(
                key: nameof(StudentGuardian.GuardianId),
                values: "Id is required");


            var expectedStudentGuardianValidationException =
                new StudentGuardianProcessingValidationException(
                    invalidStudentGuardianException);

            // when
            Action verifyPrimaryStudentGuardianExistsTask = () =>
                this.studentGuardianProcessingService.VerifyPrimaryStudentGuardianExists(
                    invalidStudentId,
                    invalidGuardianId);

            // then
            Assert.Throws<StudentGuardianProcessingValidationException>
                (verifyPrimaryStudentGuardianExistsTask);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentGuardianServiceMock.VerifyNoOtherCalls();
        }
    }
}
