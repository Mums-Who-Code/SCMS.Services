// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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
    }
}
