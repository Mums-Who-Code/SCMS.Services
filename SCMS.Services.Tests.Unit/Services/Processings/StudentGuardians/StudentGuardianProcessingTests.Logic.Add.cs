// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public async Task ShouldAddStudentGuardianAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian(); ;
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            StudentGuardian persistedStudentGuardian = inputStudentGuardian;
            StudentGuardian expectedStudentGuardian = persistedStudentGuardian.DeepClone();

            this.studentGuardianServiceMock.Setup(service =>
                service.AddStudentGuardianAsync(inputStudentGuardian))
                    .ReturnsAsync(persistedStudentGuardian);

            // when
            StudentGuardian actualStudentGuardian =
                await this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(inputStudentGuardian);

            // then
            actualStudentGuardian.Should().BeEquivalentTo(
                expectedStudentGuardian);

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(inputStudentGuardian),
                    Times.Once);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}