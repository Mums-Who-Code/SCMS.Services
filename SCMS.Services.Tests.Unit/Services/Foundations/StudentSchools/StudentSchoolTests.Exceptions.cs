// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            StudentSchool someStudentSchool = CreateRandomStudentSchool();
            SqlException sqlException = GetSqlException();

            var failedStudentSchoolStorageException =
                new FailedStudentSchoolStorageException(sqlException);

            var expectedStudentSchoolDependencyException =
                new StudentSchoolDependencyException(failedStudentSchoolStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(sqlException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchool(someStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolDependencyException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentSchoolDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfStudentSchoolAlreadyExistsAndLogItAsync()
        {
            // given
            StudentSchool someStudentSchool = CreateRandomStudentSchool();
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsStudentSchoolException =
                new AlreadyExistsStudentSchoolException(duplicateKeyException);

            var expectedStudentSchoolDependencyValidationException =
                new StudentSchoolDependencyValidationException(
                    alreadyExistsStudentSchoolException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(duplicateKeyException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchool(someStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolDependencyValidationException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            StudentSchool someStudentSchool = CreateRandomStudentSchool();
            var databaseUpdateException = new DbUpdateException();

            var failedStudentSchoolStorageException =
                new FailedStudentSchoolStorageException(databaseUpdateException);

            var expectedStudentSchoolDependencyException =
                new StudentSchoolDependencyException(failedStudentSchoolStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime())
                    .Throws(databaseUpdateException);

            // when
            ValueTask<StudentSchool> addStudentSchoolTask =
                this.studentSchoolService.AddStudentSchool(someStudentSchool);

            // then
            await Assert.ThrowsAsync<StudentSchoolDependencyException>(() =>
                addStudentSchoolTask.AsTask());

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTime(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedStudentSchoolDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertStudentSchoolAsync(It.IsAny<StudentSchool>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
