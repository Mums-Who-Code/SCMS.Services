// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Linq;
using FluentAssertions;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllStudentGuardians()
        {
            // given
            IQueryable<StudentGuardian> randomStudentGuardians =
                CreateRandomStudentGuardians();

            IQueryable<StudentGuardian> returningStudentGuardians =
                randomStudentGuardians;

            IQueryable<StudentGuardian> expectedStudentGuardians =
                returningStudentGuardians;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllStudentGuardians())
                    .Returns(returningStudentGuardians);

            // when
            IQueryable<StudentGuardian> actualStudentGuardians =
                this.studentGuardianService.RetrieveAllStudentGuardians();

            // then
            actualStudentGuardians.Should()
                .BeEquivalentTo(expectedStudentGuardians);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllStudentGuardians(),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
