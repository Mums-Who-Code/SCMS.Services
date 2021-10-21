// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
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
        }

        private SchoolValidationException CreateAndLogValidationException(Xeption exception)
        {
            var schoolValidationException = new SchoolValidationException(exception);
            this.loggingBroker.LogError(schoolValidationException);

            return schoolValidationException;
        }
    }
}
