// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.Schools;
using System.Linq;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Schools
{
    public partial class SchoolServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllSchoolsAsync()
        {
            // given
            IQueryable<School> randomSchools = CreateRandomSchools();
            IQueryable<School> retrievedSchools = randomSchools;
            IQueryable<School> expectedSchools = retrievedSchools.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSchools())
                    .Returns(retrievedSchools);

            // when
            IQueryable<School> actualSchools =
                this.schoolService.RetrieveAllSchools();

            // then
            actualSchools.Should()
                .BeEquivalentTo(expectedSchools);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSchools(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
