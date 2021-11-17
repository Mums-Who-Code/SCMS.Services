// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
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
        public void ShouldVerifyPrimaryStudentGuardianExists()
        {
            // given
            StudentGuardian randomStudentGuardian =
                CreateRandomStudentGuardian();

            StudentGuardian primaryStudentGuardian =
                randomStudentGuardian;

            primaryStudentGuardian.Level = ContactLevel.Primary;
            Guid inputStudentId = primaryStudentGuardian.StudentId;
            Guid inputGuardianId = primaryStudentGuardian.GuardianId;

            StudentGuardian expectedStudentGuardian =
                primaryStudentGuardian.DeepClone();

            IQueryable<StudentGuardian> randomStudentGuardians =
                CreateRandomStudentGuardiansWithStudentGuardian(
                    primaryStudentGuardian);

            IQueryable<StudentGuardian> storageStudentGuardians =
                randomStudentGuardians;

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Returns(storageStudentGuardians);

            // when
            StudentGuardian actualStudentGuardian =
                this.studentGuardianProcessingService
                    .VerifyNoPrimaryStudentGuardianExists(
                        inputStudentId,
                        inputGuardianId);

            // then
            actualStudentGuardian.Should().BeEquivalentTo(
                expectedStudentGuardian);

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
