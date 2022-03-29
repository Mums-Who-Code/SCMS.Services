// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveGuardianByIdAsync()
        {
            // given
            Guardian randomGuardian = CreateRandomGuardian();
            Guid inputGuardianId = randomGuardian.Id;
            Guardian returningGuardian = randomGuardian;

            Guardian expectedGuardian =
                returningGuardian.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectGuardianByIdAsync(inputGuardianId))
                    .ReturnsAsync(returningGuardian);

            // when
            Guardian actualGuardian = await
                this.guardianService.RetrieveGuardianByIdAsync(
                    inputGuardianId);

            // then
            actualGuardian.Should().
                BeEquivalentTo(expectedGuardian);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuardianByIdAsync(inputGuardianId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
