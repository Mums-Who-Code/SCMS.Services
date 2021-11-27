// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Phones
{
    public partial class PhoneServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Phone somePhone = CreateRandomPhone();
            SqlException sqlException = GetSqlException();

            var failedPhoneStorageException =
                new FailedPhoneStorageException(sqlException);

            var expectedPhoneDependencyException =
                new PhoneDependencyException(failedPhoneStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(somePhone);

            // then
            await Assert.ThrowsAsync<PhoneDependencyException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPhoneDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfPhoneAlreadyExistsAndLogItAsync()
        {
            // given
            Phone randomPhone = CreateRandomPhone();
            Phone alreadyExistsPhone = randomPhone;
            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            var duplicateKeyException = new DuplicateKeyException(exceptionMessage);

            var alreadyExistsPhoneException =
                new AlreadyExistsPhoneException(duplicateKeyException);

            var expectedPhoneDepdendencyValidationException =
                new PhoneDependencyValidationException(alreadyExistsPhoneException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(alreadyExistsPhone);

            // then
            await Assert.ThrowsAsync<PhoneDependencyValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedPhoneDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(alreadyExistsPhone),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyValidationExceptionOnAddIfReferenceErrorOccursAndLogItAsync()
        {
            // given
            Phone randomPhone = CreateRandomPhone();
            Phone alreadyExistsPhone = randomPhone;
            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;

            var foreignKeyConstraintConflictException =
                new ForeignKeyConstraintConflictException(exceptionMessage);

            var invalidPhoneReferenceException =
                new InvalidPhoneReferenceException(foreignKeyConstraintConflictException);

            var expectedPhoneDepdendencyValidationException =
                new PhoneDependencyValidationException(invalidPhoneReferenceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(foreignKeyConstraintConflictException);
            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(alreadyExistsPhone);

            // then
            await Assert.ThrowsAsync<PhoneDependencyValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedPhoneDepdendencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(alreadyExistsPhone),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async void ShouldThrowDependencyExceptionOnAddIfDbExceptionOccursAndLogItAsync()
        {
            // given
            Phone randomPhone = CreateRandomPhone();
            Phone inputPhone = randomPhone;
            string randomMessage = GetRandomString();
            string exceptionMessage = randomMessage;
            var dbUpdateException = new DbUpdateException(exceptionMessage);

            var failedPhoneStorageException =
                new FailedPhoneStorageException(dbUpdateException);

            var expectedPhoneDepdendencyException =
                new PhoneDependencyException(failedPhoneStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(dbUpdateException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(inputPhone);

            // then
            await Assert.ThrowsAsync<PhoneDependencyException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogError(It.Is(SameExceptionAs(
                   expectedPhoneDepdendencyException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(inputPhone),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
