// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Microsoft.Data.SqlClient;
using Moq;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogItAsync()
        {
            //given
            SqlException sqlException = GetSqlException();

            var failedStudentStorageException = 
                new FailedStudentStorageException(sqlException);

            var expectedStudentDependencyException = 
                new StudentDependencyException(failedStudentStorageException);

            this.storageBrokerMock.Setup(broker=>
                broker.SelectAllStudents()).Throws(sqlException);

            //when
            Action retrieveAllStudentsAction = () =>
                this.studentService.RetrieveAllStudents();

            //then
            Assert.Throws<StudentDependencyException>(retrieveAllStudentsAction);

            this.storageBrokerMock.Verify(broker=>
                broker.SelectAllStudents(),Times.Once);

            this.loggingBrokerMock.Verify(broker=>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedStudentDependencyException))),Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
