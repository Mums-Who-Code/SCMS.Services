// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianService : IStudentGuardianService
    {
        private delegate ValueTask<StudentGuardian> ReturningStudentGuardianFunction();
        private delegate IQueryable<StudentGuardian> ReturningStudentGuardiansFunction();

        private async ValueTask<StudentGuardian> TryCatch(ReturningStudentGuardianFunction
            returningStudentGuardianFunction)
        {
            try
            {
                return await returningStudentGuardianFunction();
            }
            catch (NullStudentGuardianException nullStudentGuardianException)
            {
                throw CreateAndLogValidationException(nullStudentGuardianException);
            }
            catch (InvalidStudentGuardianException invalidStudentGuardianException)
            {
                throw CreateAndLogValidationException(invalidStudentGuardianException);
            }
            catch (SqlException sqlException)
            {
                var failedStudentGuardianStorageException =
                    new FailedStudentGuardianStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedStudentGuardianStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsStudentGuardianException =
                    new AlreadyExistsStudentGuardianException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(
                    alreadyExistsStudentGuardianException);
            }
            catch (ForeignKeyConstraintConflictException foreignKeyConstraintConflictException)
            {
                var invalidStudentGuardianReferenceException =
                    new InvalidStudentGuardianReferenceException(
                        foreignKeyConstraintConflictException);

                throw CreateAndLogDependencyValidationException(
                    invalidStudentGuardianReferenceException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedStudentGuardianStorageException =
                    new FailedStudentGuardianStorageException(
                        dbUpdateException);

                throw CreateAndLogDependencyException(
                    failedStudentGuardianStorageException);
            }
            catch (Exception exception)
            {
                var failedStudentGuardianServiceException =
                    new FailedStudentGuardianServiceException(exception);

                throw CreateAndLogServiceException(
                    failedStudentGuardianServiceException);
            }
        }

        private IQueryable<StudentGuardian> TryCatch(ReturningStudentGuardiansFunction
            returningStudentGuardiansFunction)
        {
            try
            {
                return returningStudentGuardiansFunction();
            }
            catch (SqlException sqlException)
            {
                var failedStudentGuardianStorageException =
                    new FailedStudentGuardianStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(
                    failedStudentGuardianStorageException);
            }
        }

        private StudentGuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentGuardianValidationException =
                new StudentGuardianValidationException(exception);

            this.loggingBroker.LogError(studentGuardianValidationException);

            return studentGuardianValidationException;
        }

        private StudentGuardianDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var studentGuardianDependencyException =
                new StudentGuardianDependencyException(exception);

            this.loggingBroker.LogCritical(studentGuardianDependencyException);

            return studentGuardianDependencyException;
        }

        private StudentGuardianDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var studentGuardianDependencyValidationException =
                new StudentGuardianDependencyValidationException(exception);

            this.loggingBroker.LogError(studentGuardianDependencyValidationException);

            return studentGuardianDependencyValidationException;
        }

        private StudentGuardianDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentGuardianDependencyException =
                new StudentGuardianDependencyException(exception);

            this.loggingBroker.LogError(studentGuardianDependencyException);

            return studentGuardianDependencyException;
        }

        private StudentGuardianServiceException CreateAndLogServiceException(Xeption exception)
        {
            var studentGuardianServiceException =
                new StudentGuardianServiceException(exception);

            this.loggingBroker.LogError(studentGuardianServiceException);

            return studentGuardianServiceException;
        }
    }
}
