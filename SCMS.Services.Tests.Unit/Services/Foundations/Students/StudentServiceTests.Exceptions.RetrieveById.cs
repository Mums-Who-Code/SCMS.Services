// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlExceptionOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            Exception sqlException = GetSqlException();

            var failedStudentDependencyException =
                new FailedStudentStorageException(
                    sqlException);

            var expectedStudentDependencyException =
                new StudentDependencyException(
                    failedStudentDependencyException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectStudentByIdAsync(It.IsAny<Guid>()))
                    .Throws(sqlException);

            // when
            ValueTask<Student> retrieveStudentByIdTask =
                this.studentService.RetrieveStudentByIdAsync(someId);

            // then
            await Assert.ThrowsAsync<StudentDependencyException>(() =>
                retrieveStudentByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectStudentByIdAsync(someId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
               broker.LogCritical(It.Is(SameExceptionAs(
                   expectedStudentDependencyException))),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
