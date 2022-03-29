// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public async Task ShouldAddPrimaryStudentGuardianAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian(); ;
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            inputStudentGuardian.Level = ContactLevel.Primary;
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
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(inputStudentGuardian),
                    Times.Once);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddNonPrimaryStudentGuardianAsync()
        {
            // given
            StudentGuardian randomStudentGuardian = CreateRandomStudentGuardian();
            StudentGuardian inputStudentGuardian = randomStudentGuardian;
            inputStudentGuardian.Level = ContactLevel.Secondary;
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
                service.RetrieveAllStudentGuardians(),
                    Times.Never);

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(inputStudentGuardian),
                    Times.Once);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}