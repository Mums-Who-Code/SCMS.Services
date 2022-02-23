// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Agreements;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Agreements
{
    public partial class AgreementServiceTests
    {
        [Fact]
        public async Task ShouldAddAgreementAsync()
        {
            //given
            Agreement randomAgreement = CreateRandomAgreement();
            Agreement inputAgreement = randomAgreement;
            Agreement persistedAgreement = inputAgreement;
            Agreement expectedAgreement = persistedAgreement.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertAgreementAsync(inputAgreement))
                    .ReturnsAsync(persistedAgreement);

            //when
            Agreement actualAgreement =
                await this.agreementService.AddAgreementAsync(inputAgreement);

            //then
            actualAgreement.Should().BeEquivalentTo(expectedAgreement);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertAgreementAsync(inputAgreement),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
