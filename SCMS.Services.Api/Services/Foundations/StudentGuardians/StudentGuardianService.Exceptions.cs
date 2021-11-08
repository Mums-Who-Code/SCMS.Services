// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
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
        }

        private StudentGuardianValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentGuardianValidationException =
                new StudentGuardianValidationException(exception);

            this.loggingBroker.LogError(studentGuardianValidationException);

            return studentGuardianValidationException;
        }
    }
}
