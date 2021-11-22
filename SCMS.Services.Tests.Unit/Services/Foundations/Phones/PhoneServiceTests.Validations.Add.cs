// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

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
    }
}
