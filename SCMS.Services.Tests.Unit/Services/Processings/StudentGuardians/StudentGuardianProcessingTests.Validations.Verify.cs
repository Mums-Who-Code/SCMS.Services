// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
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
                this.studentGuardianProcessingService
                    .VerifyNoPrimaryStudentGuardianExists(
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

        [Fact]
        public void ShouldThrowValidationExceptionOnVerifyIfPrimaryContactAlreadyExistsAndLogIt()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            inputStudentGuardian.Level = ContactLevel.Primary;

            IQueryable<StudentGuardian> randomStudentGuardians =
                CreateRandomStudentGuardiansWithStudentGuardian(inputStudentGuardian);

            IQueryable<StudentGuardian> retrievedStudentGuardians =
                randomStudentGuardians;

            Guid inputStudentId = inputStudentGuardian.StudentId;
            Guid inputGuardianId = inputStudentGuardian.GuardianId;

            var alreadyPrimaryStudentGuardianExistsException =
                new AlreadyPrimaryStudentGuardianExistsException();

            var expectedStudentGuardianValidationException =
                new StudentGuardianProcessingValidationException(
                    alreadyPrimaryStudentGuardianExistsException);

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Returns(retrievedStudentGuardians);

            // when
            Action verifyPrimaryStudentGuardianExistsTask = () =>
                this.studentGuardianProcessingService.VerifyNoPrimaryStudentGuardianExists(
                    inputStudentId,
                    inputGuardianId);

            // then
            Assert.Throws<StudentGuardianProcessingValidationException>
                (verifyPrimaryStudentGuardianExistsTask);

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationException))),
                        Times.Once);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
