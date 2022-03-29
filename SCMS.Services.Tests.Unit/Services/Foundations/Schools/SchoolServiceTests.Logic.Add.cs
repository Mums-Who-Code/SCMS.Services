// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Schools;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolServiceTests
    {
        [Fact]
        public async Task ShouldAddSchoolAsync()
        {
            // given
            DateTimeOffset randomDateTime =
                GetRandomDateTimeOffset();

            School randomSchool =
                CreateRandomSchool(
                    dates: randomDateTime);

            School inputSchool = randomSchool;
            School storageSchool = inputSchool;

            School expectedSchool =
                storageSchool.DeepClone();

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Returns(randomDateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSchoolAsync(inputSchool))
                    .ReturnsAsync(storageSchool);

            // when
            School actualSchool =
                await this.schoolService
                    .AddSchoolAsync(inputSchool);

            // then
            actualSchool.Should()
                .BeEquivalentTo(expectedSchool);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(inputSchool),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
