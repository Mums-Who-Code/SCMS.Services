// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SMCS.Services.Api.Models.Foundations.Schools;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolTests
    {
        [Fact]
        public async Task ShouldAddSchoolAsync()
        {
            // given
            School randomSchool = CreateRandomSchool();
            School inputSchool = randomSchool;
            School storageSchool = inputSchool;

            School expectedSchool =
                storageSchool.DeepClone();

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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSchoolAsync(inputSchool),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
