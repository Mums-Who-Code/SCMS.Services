// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SMCS.Services.Api.Models.Foundations.Guardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianAsync()
        {
            // given
            Guardian randomGuardian =
                CreateRandomGuardian();

            Guardian inputGuardian = randomGuardian;
            Guardian storedGuardian = inputGuardian;

            Guardian expectedGuardian =
                storedGuardian.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertGuardianAsync(inputGuardian))
                    .ReturnsAsync(storedGuardian);

            // when
            Guardian actualGuardian = await
                this.guardianService.AddGuardianAsync(
                    inputGuardian);

            // then
            actualGuardian.Should().
                BeEquivalentTo(expectedGuardian);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(
                    It.IsAny<Guardian>()),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
