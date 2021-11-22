// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Phones;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Phones
{
    public partial class PhoneServiceTests
    {
        [Fact]
        public async Task ShouldAddPhoneAsync()
        {
            // given
            Phone randomPhone = CreateRandomPhone();
            Phone inputPhone = randomPhone;
            Phone storagePhone = inputPhone;
            Phone expectedPhone = storagePhone.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPhoneAsync(inputPhone))
                    .ReturnsAsync(storagePhone);

            // when
            Phone actualPhone = await this.phoneService
                    .AddPhoneAsync(inputPhone);

            // then
            actualPhone.Should().BeEquivalentTo(expectedPhone);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(inputPhone),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
