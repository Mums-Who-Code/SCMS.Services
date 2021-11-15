// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianService : IStudentGuardianService
    {
        private delegate ValueTask<StudentGuardian> ReturningStudentGuardianFunction();

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
    }
}
