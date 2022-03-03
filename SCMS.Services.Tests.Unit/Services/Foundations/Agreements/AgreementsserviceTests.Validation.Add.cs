// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Tests.Unit.Services.Foundations.Agreements.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements
{
    public partial class AgreementServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfAgreementNullAndLogIt()
        {
            //given
            Agreement nullAgreement = null;

            var nullAgreementException = 
                new NullAgreementException();
            var expectedPersonValidationException =
                new PersonValidationException(nullAgreementException);

            //when
            ValueTask<Agreement> addAgreementTask =
            this.agreementService.AddAgreementAsync(nullAgreement);

            //then
            await  Assert.ThrowsAsync<PersonValidationException>(() =>
            addAgreementTask.AsTask());
           
            this.logginbrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedPersonValidationException))),
            Times.Once);

            this.storageBrokerMock.Verify(storageBroker =>
            storageBroker.InsertAgreementAsync(It.IsAny<Agreement>()),
            Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.logginbrokerMock.VerifyNoOtherCalls();
        }
    } 
}    