// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentGuardianIsNullAndLogItAsync()
        {
            // given
            StudentGuardian nullStudentGuardian = null;

            var nullStudentGuardianProcessingException =
                new NullStudentGuardianProcessingException();

            var expectedStudentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(
                    nullStudentGuardianProcessingException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(nullStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianProcessingValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingValidationException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentGuardianServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentIdIsInvalidAndLogItAsync()
        {
            // given
            StudentGuardian invalidStudentGuardian = new StudentGuardian
            {
                StudentId = Guid.Empty
            };

            var invalidStudentGuardianProcessingException =
                new InvalidStudentGuardianProcessingException();

            invalidStudentGuardianProcessingException.AddData(
                key: nameof(StudentGuardian.StudentId),
                values: "Id is required");

            var expectedStudentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(
                    invalidStudentGuardianProcessingException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianProcessingValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingValidationException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentGuardianServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfLevelIsInvalidAndLogItAsync()
        {
            // given
            StudentGuardian randomStudentGuadian = CreateRandomStudentGuardian();
            StudentGuardian invalidStudentGuardian = randomStudentGuadian;
            invalidStudentGuardian.Level = GetInvalidEnum<ContactLevel>();

            var invalidStudentGuardianProcessingException =
                new InvalidStudentGuardianProcessingException();

            invalidStudentGuardianProcessingException.AddData(
                key: nameof(StudentGuardian.Level),
                values: "Value is required");

            var expectedStudentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(
                    invalidStudentGuardianProcessingException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(invalidStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianProcessingValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingValidationException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentGuardianServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPrimaryContactAlreadyExistsAndLogItAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian existingPrimaryStudentGuardian = randomStudentGuardian;
            existingPrimaryStudentGuardian.Level = ContactLevel.Primary;
            StudentGuardian inputStudentGuardian = CreateRandomStudentGuardian();
            inputStudentGuardian.StudentId = existingPrimaryStudentGuardian.StudentId;
            inputStudentGuardian.Level = ContactLevel.Primary;

            IQueryable<StudentGuardian> randomStudentGuardians =
                CreateRandomStudentGuardiansWithStudentGuardian(
                    existingPrimaryStudentGuardian);

            IQueryable<StudentGuardian> storageStudentGuardians =
                randomStudentGuardians;

            var alreadyExistsPrimaryStudentGuardianProcessingException =
                new AlreadyExistsPrimaryStudentGuardianProcessingException(
                    existingPrimaryStudentGuardian.GuardianId);

            var expectedStudentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(
                    alreadyExistsPrimaryStudentGuardianProcessingException);

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Returns(storageStudentGuardians);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(inputStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianProcessingValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingValidationException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}