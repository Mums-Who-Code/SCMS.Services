// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Guardians.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guardian someGuardian = CreateRandomGuardian();
            SqlException sqlException = GetSqlException();

            var failedGuardianStorageException =
                new FailedGuardianStorageException(sqlException);

            var expectedGuardianDependencyException =
                new GuardianDependencyException(failedGuardianStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(someGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedGuardianDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfGuardianAlreadyExistsAndLogItAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian alreadyExistsGuardian = randomGuardian;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;
            var duplicateKeyException = new DuplicateKeyException(exceptionMessage);

            var alreadyExistsGuardianException =
                new AlreadyExistsGuardianException(duplicateKeyException);

            var expectedGuardianDepdendencyValidationException =
                new GuardianDependencyValidationException(alreadyExistsGuardianException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(alreadyExistsGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedGuardianDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(alreadyExistsGuardian),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian alreadyExistsGuardian = randomGuardian;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidGuardianReferenceException =
                new InvalidGuardianReferenceException(foreignKeyConstraintConflictException);

            var expectedGuardianDepdendencyValidationException =
                new GuardianDependencyValidationException(invalidGuardianReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(foreignKeyConstraintConflictException);
            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(alreadyExistsGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedGuardianDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(alreadyExistsGuardian),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyExceptionOnAddIfDbExceptionOccursAndLogItAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian inputGuardian = randomGuardian;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;
            var dbUpdateException = new DbUpdateException(exceptionMessage);

            var failedGuardianStorageException =
                new FailedGuardianStorageException(dbUpdateException);

            var expectedGuardianDepdendencyException =
                new GuardianDependencyException(failedGuardianStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(dbUpdateException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(inputGuardian);

            // then
            await Assert.ThrowsAsync<GuardianDependencyException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedGuardianDepdendencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(inputGuardian),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian inputGuardian = randomGuardian;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;
            var serviceException = new Exception(exceptionMessage);

            var failedGuardianServiceException =
                new FailedGuardianServiceException(serviceException);

            var expectedGuardianServiceException =
                new GuardianServiceException(failedGuardianServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(serviceException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(inputGuardian);

            // then
            await Assert.ThrowsAsync<GuardianServiceException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedGuardianServiceException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(inputGuardian),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
