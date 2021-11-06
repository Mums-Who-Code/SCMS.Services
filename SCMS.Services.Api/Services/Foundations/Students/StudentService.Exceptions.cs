// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.Students
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
                var failedStudentStorageException =
                    new FailedStudentStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedStudentStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsStudentException =
                    new AlreadyExistsStudentException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsStudentException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedStudentStorageException =
                    new FailedStudentStorageException(
                        dbUpdateException);

                throw CreateAndLogDependencyException(
                    failedStudentStorageException);
            }
            catch (Exception exception)
            {
                var failedStudentServiceException =
                    new FailedStudentServiceException(
                        exception);

                throw CreateAndLogServiceException(
                    failedStudentServiceException);
            }
        }

        private StudentValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentValidationException = new StudentValidationException(exception);
            this.loggingBroker.LogError(studentValidationException);

            return studentValidationException;
        }

        private StudentDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);
            this.loggingBroker.LogCritical(studentDependencyException);

            return studentDependencyException;
        }

        private StudentDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var studentDependencyValidationException = new StudentDependencyValidationException(exception);
            this.loggingBroker.LogError(studentDependencyValidationException);

            return studentDependencyValidationException;
        }

        private StudentDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentDependencyException = new StudentDependencyException(exception);
            this.loggingBroker.LogError(studentDependencyException);

            return studentDependencyException;
        }

        private StudentServiceException CreateAndLogServiceException(Xeption exception)
        {
            var studentServiceException = new StudentServiceException(exception);
            this.loggingBroker.LogError(studentServiceException);

            return studentServiceException;
        }
    }
}
