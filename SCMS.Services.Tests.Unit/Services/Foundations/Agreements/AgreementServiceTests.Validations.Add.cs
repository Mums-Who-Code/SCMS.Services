// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Models.Foundations.Agreements.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements
{
    public partial class AgreementServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfAgreementIsNullAndLogItAsync()
        {
            // given
            Agreement nullArgeement = null;
            var nullArgeementException = new NullAgreementException();

            var expectedAgreementValidationException =
                new AgreementValidationException(nullArgeementException);

            // when
            ValueTask<Agreement> addAgreementTask =
                this.agreementService.AddAgreementAsync(nullArgeement);

            // then
            await Assert.ThrowsAsync<AgreementValidationException>(() =>
                addAgreementTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAgreementValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertAgreementAsync(It.IsAny<Agreement>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfAgreementIsInvalidAndLogItAsync()
        {
            //given
            var invalidAgreement = new Agreement
            {
                Status = GetInvalidEnum<AgreementStatus>()
            };

            var invalidAgreementException =
               new InvalidAgreementException();

            invalidAgreementException.AddData(
                key: nameof(Agreement.Id),
                values: "Id is required");

            invalidAgreementException.AddData(
                key: nameof(Agreement.Status),
                values: "Value is not recognized");

            var expectedAgreementValidationException =
                new AgreementValidationException(invalidAgreementException);

            //when
            ValueTask<Agreement> addAgreementTask =
                this.agreementService.AddAgreementAsync(invalidAgreement);

            //then
            await Assert.ThrowsAsync<AgreementValidationException>(() =>
                addAgreementTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAgreementValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertAgreementAsync(It.IsAny<Agreement>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
