// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Phones
{
    public partial class PhoneServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPhoneIsNullAndLogItAsync()
        {
            // given
            Phone nullPhone = null;
            var nullPhoneException = new NullPhoneException();

            var expectedPhoneValidationException =
                new PhoneValidationException(nullPhoneException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(nullPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfPhoneIsInvalidAndLogItAsync(
            string invalidValue)
        {
            // given
            var invalidPhone = new Phone
            {
                CountryCode = invalidValue,
                Number = invalidValue
            };

            var invalidPhoneException = new InvalidPhoneException();

            invalidPhoneException.AddData(
                key: nameof(Phone.Id),
                values: "Id is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.CountryCode),
                values: "Text is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.Number),
                values: "Text is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.CreatedDate),
                values: "Date is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.UpdatedDate),
                values: "Date is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.CreatedBy),
                values: "Id is required.");

            invalidPhoneException.AddData(
                key: nameof(Phone.UpdatedBy),
                values: "Id is required.");

            var expectedPhoneValidationException =
                new PhoneValidationException(invalidPhoneException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(invalidPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfNumberIsInvalidAndLogItAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Phone randomPhone = CreateRandomPhone(dates: randomDateTime);
            Phone invalidPhone = randomPhone;
            invalidPhone.Number = GetRandomString();

            var invalidPhoneException = new InvalidPhoneException();

            invalidPhoneException.AddData(
                key: nameof(Phone.Number),
                values: "Text is invalid.");

            var expectedPhoneValidationException =
                new PhoneValidationException(invalidPhoneException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(invalidPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            // given
            Phone randomPhone = CreateRandomPhone();
            Phone invalidPhone = randomPhone;
            invalidPhone.UpdatedDate = invalidPhone.CreatedDate.AddDays(1);
            var invalidPhoneException = new InvalidPhoneException();

            invalidPhoneException.AddData(
                key: nameof(Phone.UpdatedDate),
                values: $"Date is not same as {nameof(Phone.CreatedDate)}.");

            var expectedPhoneValidationException =
                new PhoneValidationException(invalidPhoneException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(invalidPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedByIsNotSameAsByAndLogItAsync()
        {
            // given
            Guid randomGuid = Guid.NewGuid();
            Phone randomPhone = CreateRandomPhone();
            Phone invalidPhone = randomPhone;
            invalidPhone.UpdatedBy = randomGuid;
            var invalidPhoneException = new InvalidPhoneException();

            invalidPhoneException.AddData(
                key: nameof(Phone.UpdatedBy),
                values: $"Id is not same as {nameof(Phone.CreatedBy)}.");

            var expectedPhoneValidationException =
                new PhoneValidationException(invalidPhoneException);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(invalidPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidMinuteCases))]
        public async void ShouldThrowValidationExceptionOnCreateIfCreatedDateIsNotRecentAndLogItAsync(
            int minutes)
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTime();
            Phone randomPhone = CreateRandomPhone(dates: randomDateTime);
            Phone invalidPhone = randomPhone;
            invalidPhone.CreatedDate = invalidPhone.CreatedDate.AddMinutes(minutes);
            invalidPhone.UpdatedDate = invalidPhone.CreatedDate;
            var invalidPhoneException = new InvalidPhoneException();

            invalidPhoneException.AddData(
                key: nameof(Phone.CreatedDate),
                values: $"Date is not recent.");

            var expectedPhoneValidationException =
                new PhoneValidationException(invalidPhoneException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            // when
            ValueTask<Phone> addPhoneTask =
                this.phoneService.AddPhoneAsync(invalidPhone);

            // then
            await Assert.ThrowsAsync<PhoneValidationException>(() =>
                addPhoneTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPhoneValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(It.IsAny<Phone>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
