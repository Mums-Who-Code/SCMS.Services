// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlExceptionOccursAndLogItAsync()
        {
            //given
            DateTimeOffset randonDateTime = GetRandomDateTime();

            TermsAndCondition randomTermsAndCondition =
                CreateRandomTermsAndCondition(randonDateTime);

            TermsAndCondition inputTermsAndCondition = randomTermsAndCondition;
            SqlException sqlException = GetSqlException();

            var failedTermsAndConditionDependencyException =
                new FailedTermsAndConditionStorageException(sqlException);

            var expectedTermsAndConditionDependencyException =
                new TermsAndConditionDependencyException(
                    failedTermsAndConditionDependencyException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            //when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(
                    inputTermsAndCondition);

            //then
            await Assert.ThrowsAsync<TermsAndConditionDependencyException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedTermsAndConditionDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(inputTermsAndCondition),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfTermsAndConditionAlreadyExistsAndLogItAsync()
        {
            // given
            TermsAndCondition randomTermsAndCondition = CreateRandomTermsAndCondition();
            TermsAndCondition alreadyExistsTermsAndCondition = randomTermsAndCondition;
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsTermsAndConditionException =
                new AlreadyExistsTermsAndConditionException(
                    duplicateKeyException);

            var expectedTermsAndConditionDependencyValidation =
                new TermsAndConditionDependencyValidationException(
                    alreadyExistsTermsAndConditionException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(
                    alreadyExistsTermsAndCondition);

            // then
            await Assert.ThrowsAsync<TermsAndConditionDependencyValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionDependencyValidation))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            TermsAndCondition randomTermsAndCondition = CreateRandomTermsAndCondition();
            TermsAndCondition alreadyExistsTermsAndCondition = randomTermsAndCondition;
            string randomMessage = GetRandomMessage();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
               new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidTermsAndConditionReferenceException =
                new InvalidTermsAndConditionReferenceException(
                    foreignKeyConstraintConflictException);

            var expectedTermsAndConditionDependencyValidationException =
               new TermsAndConditionDependencyValidationException(
                   invalidTermsAndConditionReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(foreignKeyConstraintConflictException);

            //when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(
                    alreadyExistsTermsAndCondition);

            //then
            await Assert.ThrowsAsync<TermsAndConditionDependencyValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(alreadyExistsTermsAndCondition),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            TermsAndCondition someTermsAndCondition = CreateRandomTermsAndCondition();
            var databaseUpdateException = new DbUpdateException();

            var failedTermsAndConditionStorageException =
                new FailedTermsAndConditionStorageException(databaseUpdateException);

            var expectedTermsAndConditionDependencyException =
                new TermsAndConditionDependencyException(failedTermsAndConditionStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(someTermsAndCondition);

            // then
            await Assert.ThrowsAsync<TermsAndConditionDependencyException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
