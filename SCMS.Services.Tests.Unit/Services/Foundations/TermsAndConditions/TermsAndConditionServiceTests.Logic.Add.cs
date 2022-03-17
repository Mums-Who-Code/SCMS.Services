// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
            DateTimeOffset randomDateTime = GetRandomDateTime();

            TermsAndCondition randomTermsAndCondition =
                CreateRandomTermsAndCondition(randomDateTime);

            TermsAndCondition inputTermsAndCondition =
                randomTermsAndCondition;

            TermsAndCondition storedTermsAndCondition =
                inputTermsAndCondition;

            TermsAndCondition expectedTermsAndCondition =
                storedTermsAndCondition.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

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

            this.dateTimeBrokerMock.Verify(broker =>
               broker.GetCurrentDateTime(),
                   Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertTermsAndConditionAsync(
                     It.IsAny<TermsAndCondition>()),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
