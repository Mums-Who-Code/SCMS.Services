// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
using Xeptions;

namespace SMCS.Services.Api.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolService
    {
        private delegate ValueTask<StudentSchool> ReturningStudentSchoolFunction();

        private
            async ValueTask<StudentSchool> TryCatch(
            ReturningStudentSchoolFunction returningStudentSchoolFunction)
        {
            try
            {
                return await returningStudentSchoolFunction();
            }
            catch (NullStudentSchoolException nullStudentSchoolException)
            {
                throw CreateAndLogValidationException(nullStudentSchoolException);
            }
            catch (InvalidStudentSchoolException invalidStudentSchoolException)
            {
                throw CreateAndLogValidationException(invalidStudentSchoolException);
            }
            catch (SqlException sqlException)
            {
                var failedStudentSchoolStorageException =
                    new FailedStudentSchoolStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedStudentSchoolStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsStudentSchoolException =
                    new AlreadyExistsStudentSchoolException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistsStudentSchoolException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidStudentSchoolReferenceException =
                    new InvalidStudentSchoolReferenceException(foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyException(invalidStudentSchoolReferenceException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedStudentSchoolStorageException =
                    new FailedStudentSchoolStorageException(databaseUpdateException);

                throw CreateAndLogDependencyException(failedStudentSchoolStorageException);
            }
            catch (Exception exception)
            {
                var failedStudentSchoolServiceException =
                    new FailedStudentSchoolServiceException(exception);

                throw CreateAndLogServiceException(failedStudentSchoolServiceException);
            }
        }

        private StudentSchoolValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentSchoolValidationException =
                new StudentSchoolValidationException(exception);

            this.loggingBroker.LogError(studentSchoolValidationException);

            return studentSchoolValidationException;
        }

        private StudentSchoolDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var studentSchoolDependencyException =
                new StudentSchoolDependencyException(exception);

            this.loggingBroker.LogCritical(studentSchoolDependencyException);

            return studentSchoolDependencyException;
        }

        private StudentSchoolDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var studentSchoolDependencyValidationException =
                new StudentSchoolDependencyValidationException(exception);

            this.loggingBroker.LogError(studentSchoolDependencyValidationException);

            return studentSchoolDependencyValidationException;
        }

        private StudentSchoolDependencyException CreateAndLogDependencyException(
            Xeption exception)
        {
            var studentSchoolDependencyException =
                new StudentSchoolDependencyException(exception);

            this.loggingBroker.LogError(studentSchoolDependencyException);

            return studentSchoolDependencyException;
        }

        private StudentSchoolServiceException CreateAndLogServiceException(
            Xeption exception)
        {
            var studentSchoolServiceException =
                new StudentSchoolServiceException(exception);

            this.loggingBroker.LogError(studentSchoolServiceException);

            return studentSchoolServiceException;
        }
    }
}
