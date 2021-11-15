// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();
            SqlException sqlException = GetSqlException();

            var failedStudentGuardianStorageException =
                new FailedStudentGuardianStorageException(sqlException);

            var expectedStudentGuardianDependencyException =
                new StudentGuardianDependencyException(failedStudentGuardianStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(someStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianDependencyException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentGuardianDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfStudentGuardianAlreadyExistsAndLogItAsync()
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();
            string someMessage = GetRandomMessage();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsStudentGuardianException =
                new AlreadyExistsStudentGuardianException(duplicateKeyException);

            var expectedStudentGuardianDependencyValidationException =
                new StudentGuardianDependencyValidationException(alreadyExistsStudentGuardianException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(someStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianDependencyValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian alreadyExistsStudentGuardian = randomStudentGuardian;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidStudentGuardianReferenceException =
                new InvalidStudentGuardianReferenceException(foreignKeyConstraintConflictException);

            var expectedStudentGuardianDepdendencyValidationException =
                new StudentGuardianDependencyValidationException(invalidStudentGuardianReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(foreignKeyConstraintConflictException);
            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(alreadyExistsStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianDependencyValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedStudentGuardianDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(alreadyExistsStudentGuardian),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();
            var databaseUpdateException = new DbUpdateException();

            var failedStudentGuardianStorageException =
                new FailedStudentGuardianStorageException(databaseUpdateException);

            var expectedStudentGuardianDependencyException =
                new StudentGuardianDependencyException(failedStudentGuardianStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(someStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianDependencyException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();
            var serviceException = new Exception();

            var failedStudentGuardianServiceException =
                new FailedStudentGuardianServiceException(serviceException);

            var expectedStudentGuardianServiceException =
                new StudentGuardianServiceException(failedStudentGuardianServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(serviceException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianService.AddStudentGuardianAsync(someStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianServiceException>(() =>
                addStudentGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianServiceException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
