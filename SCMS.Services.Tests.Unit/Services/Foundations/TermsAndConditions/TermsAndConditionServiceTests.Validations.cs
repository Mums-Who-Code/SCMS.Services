// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfTermsAndConditionIsNullAndLogItAsync()
        {
            // given
            TermsAndCondition nullTermsAndCondition = null;

            var nullTermsAndConditionException =
                new NullTermsAndConditionException();

            var expectedTermsAndConditionValidationException =
                new TermsAndConditionValidationException(nullTermsAndConditionException);

            // when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(nullTermsAndCondition);

            // then
            await Assert.ThrowsAsync<TermsAndConditionValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddIfTermsAndConditionIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidTermsAndCondition = new TermsAndCondition
            {
                Name = invalidText,
                Type = GetInvalidEnum<TermsAndConditionType>()
            };

            var invalidTermsAndConditionException =
                new InvalidTermsAndConditionException();

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.Id),
                values: "Id is required.");

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.Name),
                values: "Text is invalid.");

            invalidTermsAndConditionException.AddData(
                 key: nameof(TermsAndCondition.Url),
                 values: "Text is invalid.");

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.Type),
                values: "Value is not recognized.");

            invalidTermsAndConditionException.AddData(
                 key: nameof(TermsAndCondition.CreatedBy),
                 values: "Id is required.");

            invalidTermsAndConditionException.AddData(
                 key: nameof(TermsAndCondition.UpdatedBy),
                 values: "Id is required.");

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.CreatedDate),
                values: "Date is required.");

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.UpdatedDate),
                values: "Date is required.");


            var expectedTermsAndConditionValidationException =
                new TermsAndConditionValidationException(invalidTermsAndConditionException);

            // when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(invalidTermsAndCondition);

            // then
            await Assert.ThrowsAsync<TermsAndConditionValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTime(),
                   Times.Never);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(InvalidUrls))]
        public async Task ShouldThrowValidationExceptionOnAddIfUrlIsInvalidAndLogItAsync(
            string invalidUrl)
        {
            //given
            TermsAndCondition randomTermsAndCondition = CreateRandomTermsAndCondition();
            TermsAndCondition invalidTermsAndCondition = randomTermsAndCondition;
            invalidTermsAndCondition.Url = invalidUrl;

            var invalidTermsAndConditionException =
               new InvalidTermsAndConditionException();

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.Url),
                values: "Text is invalid.");

            var expectedTermsAndConditionValidationException =
                new TermsAndConditionValidationException(invalidTermsAndConditionException);

            //when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(invalidTermsAndCondition);

            //then
            await Assert.ThrowsAsync<TermsAndConditionValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfCreatedDateIsNotSameAsUpdatedDateAndLogItAsync()
        {
            //given
            int randomDays = GetRandomNumber();
            TermsAndCondition randomTermsAndCondition = CreateRandomTermsAndCondition();
            TermsAndCondition invalidTermsAndCondition = randomTermsAndCondition;

            invalidTermsAndCondition.UpdatedDate =
                invalidTermsAndCondition.UpdatedDate.AddDays(randomDays);

            var invalidTermsAndConditionException =
               new InvalidTermsAndConditionException();

            invalidTermsAndConditionException.AddData(
                key: nameof(TermsAndCondition.UpdatedDate),
                values: $"Date is not same as {nameof(TermsAndCondition.CreatedDate)}.");

            var expectedTermsAndConditionValidationException =
                new TermsAndConditionValidationException(invalidTermsAndConditionException);

            //when
            ValueTask<TermsAndCondition> addTermsAndConditionTask =
                this.termsAndConditionService.AddTermsAndConditionAsync(invalidTermsAndCondition);

            //then
            await Assert.ThrowsAsync<TermsAndConditionValidationException>(() =>
                addTermsAndConditionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedTermsAndConditionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(It.IsAny<TermsAndCondition>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}