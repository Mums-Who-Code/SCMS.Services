// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Processings.Students
{
    public partial class StudentProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnVerifyIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid inputStudentId = Guid.Empty;
            var invalidStudentProcessingException = new InvalidStudentProcessingException();

            invalidStudentProcessingException.AddData(
                key: nameof(Student.Id),
                values: "Id is required");

            var expectedStudentProcessingValidation = new StudentProcessingValidationException(
                invalidStudentProcessingException);

            // when
            ValueTask<Student> verifyStudentExsistsTask = this.studentProcessingService
                .VerifyStudentExistsAsync(inputStudentId);

            // then
            await Assert.ThrowsAsync<StudentProcessingValidationException>(() =>
                verifyStudentExsistsTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentProcessingValidation))),
                        Times.Once);

            this.studentServiceMock.Verify(service =>
                service.RetrieveStudentByIdAsync(inputStudentId),
                    Times.Never);

            this.studentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
