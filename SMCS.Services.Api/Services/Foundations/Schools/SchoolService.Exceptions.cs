// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Models.Foundations.Schools.Exceptions;
using Xeptions;

namespace SMCS.Services.Api.Services.Foundations.Schools
{
    public partial class SchoolService
    {
        private delegate ValueTask<School> ReturningSchoolFunction();

        private async ValueTask<School> TryCatch(ReturningSchoolFunction returningSchoolFunction)
        {
            try
            {
                return await returningSchoolFunction();
            }
            catch (NullSchoolException nullSchoolException)
            {
                throw CreateAndLogValidationException(nullSchoolException);
            }
            catch (InvalidSchoolException invalidSchoolException)
            {
                throw CreateAndLogValidationException(invalidSchoolException);
            }
            catch (SqlException sqlException)
            {
                var failedSchoolStorageException =
                    new FailedSchoolStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedSchoolStorageException);
            }
        }

        private SchoolValidationException CreateAndLogValidationException(Xeption exception)
        {
            var schoolValidationException = new SchoolValidationException(exception);
            this.loggingBroker.LogError(schoolValidationException);

            return schoolValidationException;
        }

        private SchoolDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var schoolDependencyException = new SchoolDependencyException(exception);
            this.loggingBroker.LogCritical(schoolDependencyException);

            return schoolDependencyException;
        }
    }
}
