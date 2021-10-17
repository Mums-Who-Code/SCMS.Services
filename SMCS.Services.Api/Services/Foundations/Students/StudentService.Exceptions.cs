// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SMCS.Services.Api.Models.Foundations.Students;
using SMCS.Services.Api.Models.Foundations.Students.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.Students
{
    public partial class StudentService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (NullStudentException nullStudentException)
            {
                throw CreateAndLogValidationException(nullStudentException);
            }
            catch (InvalidStudentException invalidStudentException)
            {
                throw CreateAndLogValidationException(invalidStudentException);
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
        }

        private StudentValidationException CreateAndLogValidationException(Exception exception)
        {
            var studentValidationException = new StudentValidationException(exception);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }

        private StudentDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);
            this.loggingBroker.LogCritical(studentDependencyException);

            return studentDependencyException;
        }
    }
}
