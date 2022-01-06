// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.Students
{
    public partial class StudentProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnVerifyIfValidationErrorOccursAndLogItAsync()
        {
            // given
            Guid someStudentId = Guid.NewGuid();
            var invalidStudentException = new InvalidStudentException();
            var studentValidationException = new StudentValidationException(invalidStudentException);

            var expectedStudentProcessingDependencyValidation =
                new StudentProcessingDependencyValidationException(
                    studentValidationException.InnerException as Xeption);

            this.studentServiceMock.Setup(service =>
                service.RetrieveStudentByIdAsync(someStudentId))
                    .ThrowsAsync(studentValidationException);

            // when
            ValueTask<Student> verifyStudentExsistsTask = this.studentProcessingService
                .VerifyStudentExistsAsync(someStudentId);

            // then
            await Assert.ThrowsAsync<StudentProcessingDependencyValidationException>(() =>
                verifyStudentExsistsTask.AsTask());

            this.studentServiceMock.Verify(service =>
                service.RetrieveStudentByIdAsync(someStudentId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentProcessingDependencyValidation))),
                        Times.Once);

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
