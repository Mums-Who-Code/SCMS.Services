// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Guardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Guardians
{
    public partial class GuardianServiceTests
    {
        [Fact]
        public async Task ShouldAddGuardianAsync()
        {
            // given
            DateTimeOffset randomDate =
                GetRandomDateTime();

            Guardian randomGuardian =
                CreateRandomGuardian(randomDate);

            Guardian inputGuardian = randomGuardian;
            Guardian storedGuardian = inputGuardian;

            Guardian expectedGuardian =
                storedGuardian.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDate);

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

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuardianAsync(
                    It.IsAny<Guardian>()),
                        Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
