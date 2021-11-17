// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using FluentAssertions;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public void ShouldVerifyNoPrimaryStudentGuardianExists()
        {
            // given
            Guid inputStudentId = Guid.NewGuid();
            Guid inputGuardianId = Guid.NewGuid();
            StudentGuardian expectedStudentGuardian = null;

            IQueryable<StudentGuardian> randomStudentGuardians =
                CreateRandomStudentGuardians();

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
