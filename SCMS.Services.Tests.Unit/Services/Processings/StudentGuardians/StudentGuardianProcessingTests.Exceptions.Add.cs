// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Theory]
        [MemberData(nameof(DependencyApiExceptions))]
        public void ShouldThrowDependencyExceptionOnVerifyIfDependencyErrorOccursndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();

            var expectedStudentGuardianProcessingDependencyException =
                new StudentGuardianProcessingDependencyException(
                    dependencyValidationException);

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Throws(dependencyValidationException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(someStudentGuardian);

            // then
            Assert.ThrowsAsync<StudentGuardianProcessingDependencyException>(() =>
                addStudentGuardianTask.AsTask());

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingDependencyException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyValidationExceptions))]
        public void ShouldThrowDependencyExceptionOnVerifyIfDependencyValidationErrorOccursndLogItAsync(
            Xeption dependencyValidationException)
        {
            // given
            StudentGuardian someStudentGuardian = CreateRandomStudentGuardian();

            var expectedStudentGuardianProcessingDependencyValidationException =
                new StudentGuardianProcessingDependencyValidationException(
                    dependencyValidationException.InnerException as Xeption);

            this.studentGuardianServiceMock.Setup(service =>
                service.RetrieveAllStudentGuardians())
                    .Throws(dependencyValidationException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(someStudentGuardian);

            // then
            Assert.ThrowsAsync<StudentGuardianProcessingDependencyValidationException>(() =>
                addStudentGuardianTask.AsTask());

            this.studentGuardianServiceMock.Verify(service =>
                service.RetrieveAllStudentGuardians(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianProcessingDependencyValidationException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.studentGuardianServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}