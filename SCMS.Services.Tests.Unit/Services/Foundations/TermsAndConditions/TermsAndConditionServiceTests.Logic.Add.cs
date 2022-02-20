// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionServiceTests
    {
        [Fact]
        public async Task ShouldAddTermsAndConditionAsync()
        {
            // given
            TermsAndCondition randomTermsAndCondition =
                CreateRandomTermsAndCondition();

            TermsAndCondition inputTermsAndCondition = randomTermsAndCondition;
            TermsAndCondition storedTermsAndCondition = inputTermsAndCondition;

            TermsAndCondition expectedTermsAndCondition =
                storedTermsAndCondition.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertTermsAndConditionAsync(inputTermsAndCondition))
                    .ReturnsAsync(storedTermsAndCondition);

            // when
            TermsAndCondition actualTermsAndCondition = await
                this.termsAndConditionService.AddTermsAndConditionAsync(
                    inputTermsAndCondition);

            // then
            actualTermsAndCondition.Should().
                BeEquivalentTo(expectedTermsAndCondition);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(
                    inputTermsAndCondition),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
