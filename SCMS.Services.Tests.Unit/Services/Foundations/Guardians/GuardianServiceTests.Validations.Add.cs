// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SMCS.Services.Api.Models.Foundations.Guardians;
using SMCS.Services.Api.Models.Foundations.Guardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianIsNullAndLogItAsync()
        {
            // given
            Guardian nullGuardian = null;
            var nullGuardianException = new NullGuardianException();

            var expectedGuardianValidationException =
                new GuardianValidationException(nullGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(nullGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(
                    SameExceptionAs(expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuardianIsInvalidAndLogItAsync(
            string invalidName)
        {
            // given
            var invalidTitle = GetInvalidEnum<Title>();

            var invalidGuardian = new Guardian
            {
                FirstName = invalidName,
                Title = invalidTitle
            };

            var invalidGuardianException = new InvalidGuardianException();

            invalidGuardianException.AddData(
                key: nameof(Guardian.Id),
                values: "Id is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.Title),
                values: "Value is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.FirstName),
                values: "Text is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.LastName),
                values: "Text is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedDate),
                values: "Date is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedBy),
                values: "Id is required.");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian invalidGuardian = randomGuardian;
            invalidGuardian.UpdateDate = invalidGuardian.CreatedDate.AddDays(1);
            var invalidGuardianException = new InvalidGuardianException();

            invalidGuardianException.AddData(
                key: nameof(Guardian.UpdateDate),
                values: $"Date is not same as {nameof(Guardian.CreatedDate)}.");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsByAndLogItAsync()
        {
            // given
            Guid randomGuid = Guid.NewGuid();
            Guardian randomGuardian = CreateRandomGuardian();
            Guardian invalidGuardian = randomGuardian;
            invalidGuardian.UpdatedBy = randomGuid;
            var invalidGuardianException = new InvalidGuardianException();

            invalidGuardianException.AddData(
                key: nameof(Guardian.UpdatedBy),
                values: $"Id is not same as {nameof(Guardian.CreatedBy)}.");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async void ShouldThrowValidationExceptionOnCreateIfCreatedDateIsNotRecentAndLogItAsync(
            int minutes)
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Guardian randomGuardian = CreateRandomGuardian(date: randomDateTime);
            Guardian invalidGuardian = randomGuardian;
            invalidGuardian.CreatedDate = invalidGuardian.CreatedDate.AddMinutes(minutes);
            invalidGuardian.UpdateDate = invalidGuardian.CreatedDate;
            var invalidGuardianException = new InvalidGuardianException();

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedDate),
                values: $"Date is not recent.");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
