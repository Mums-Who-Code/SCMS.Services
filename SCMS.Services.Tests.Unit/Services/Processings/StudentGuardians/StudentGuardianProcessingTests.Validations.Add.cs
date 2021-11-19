// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStudentGuardianIsNullAndLogItAsync()
        {
            // given
            StudentGuardian nullStudentGuardian = null;

            var nullStudentGuardianProcessingException =
                new NullStudentGuardianProcessingException();

            var expectedStudentGuardianValidationProcessingException =
                new StudentGuardianValidationProcessingException(
                    nullStudentGuardianProcessingException);

            // when
            ValueTask<StudentGuardian> addStudentGuardianTask =
                this.studentGuardianProcessingService
                    .AddStudentGuardianAsync(nullStudentGuardian);

            // then
            await Assert.ThrowsAsync<StudentGuardianValidationProcessingException>(() =>
                addStudentGuardianTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentGuardianValidationProcessingException))));

            this.studentGuardianServiceMock.Verify(service =>
                service.AddStudentGuardianAsync(It.IsAny<StudentGuardian>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.studentGuardianServiceMock.VerifyNoOtherCalls();
        }
    }
}