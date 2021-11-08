// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        [Fact]
        public async Task ShouldAddStudentGuardianAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            StudentGuardian storageStudentGuardian = inputStudentGuardian;
            StudentGuardian expectedStudentGuardian = storageStudentGuardian.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertStudentGuardianAsync(inputStudentGuardian))
                    .ReturnsAsync(storageStudentGuardian);

            // when
            StudentGuardian actualStudentGuardian =
                await this.studentGuardianService
                    .AddStudentGuardianAsync(inputStudentGuardian);

            // then
            actualStudentGuardian.Should()
                .BeEquivalentTo(expectedStudentGuardian);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentGuardianAsync(inputStudentGuardian),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
