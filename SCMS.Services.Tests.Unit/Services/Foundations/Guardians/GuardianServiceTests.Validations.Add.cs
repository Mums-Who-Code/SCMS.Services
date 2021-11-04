// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
                key: nameof(Guardian.UpdateDate),
                values: "Date is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.CreatedBy),
                values: "Id is required.");

            invalidGuardianException.AddData(
                key: nameof(Guardian.UpdatedBy),
                values: "Id is required.");

            var expectedGuardianValidationException =
                new GuardianValidationException(invalidGuardianException);

            // when
            ValueTask<Guardian> addGuardianTask =
                this.guardianService.AddGuardianAsync(invalidGuardian);

            // then
            await Assert.ThrowsAsync<GuardianValidationException>(() =>
                addGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuardianValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(It.IsAny<Guardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
