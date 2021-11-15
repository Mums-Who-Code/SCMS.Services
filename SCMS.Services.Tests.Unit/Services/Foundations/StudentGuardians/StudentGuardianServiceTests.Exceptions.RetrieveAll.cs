// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedStudentGuardianStorageException =
                new FailedStudentGuardianStorageException(sqlException);

            var expectedStudentGuardianDependencyException =
                new StudentGuardianDependencyException(failedStudentGuardianStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllStudentGuardians())
                    .Throws(sqlException);

            // when
            Action retrieveAllStudentGuardiansTask = () =>
                this.studentGuardianService.RetrieveAllStudentGuardians();

            // then
            Assert.Throws<StudentGuardianDependencyException>(retrieveAllStudentGuardiansTask);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentGuardianDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllStudentGuardians(),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
