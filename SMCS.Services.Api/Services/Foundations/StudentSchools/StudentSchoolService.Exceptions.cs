// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;
using Xeptions;

namespace SMCS.Services.Api.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolService
    {
        private delegate ValueTask<StudentSchool> ReturningStudentSchoolFunction();

        private async ValueTask<StudentSchool> TryCatch(
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
        }

        private StudentSchoolValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentSchoolValidationException =
                new StudentSchoolValidationException(exception);

            this.loggingBroker.LogError(studentSchoolValidationException);

            return studentSchoolValidationException;
        }
    }
}
