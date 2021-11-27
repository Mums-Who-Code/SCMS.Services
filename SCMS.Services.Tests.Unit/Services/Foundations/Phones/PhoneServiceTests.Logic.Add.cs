// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
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
            DateTimeOffset randomDate = GetRandomDateTime();
            Phone randomPhone = CreateRandomPhone(dates: randomDate);
            Phone inputPhone = randomPhone;
            Phone storagePhone = inputPhone;
            Phone expectedPhone = storagePhone.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPhoneAsync(inputPhone))
                    .ReturnsAsync(storagePhone);

            // when
            Phone actualPhone = await this.phoneService
                    .AddPhoneAsync(inputPhone);

            // then
            actualPhone.Should().BeEquivalentTo(expectedPhone);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPhoneAsync(inputPhone),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
