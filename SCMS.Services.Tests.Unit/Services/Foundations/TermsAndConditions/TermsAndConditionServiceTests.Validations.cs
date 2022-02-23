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
    }
}