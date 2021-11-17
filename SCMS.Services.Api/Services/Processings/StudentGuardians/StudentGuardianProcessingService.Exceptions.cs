// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService
    {
        private delegate StudentGuardian ReturningStudentGuardianProcessingServiceFunction();

        private StudentGuardian TryCatch(ReturningStudentGuardianProcessingServiceFunction
            returningStudentGuardianProcessingServiceFunction)
        {
            try
            {
                return returningStudentGuardianProcessingServiceFunction();
            }
            catch (InvalidStudentGuardianProcessingException invalidStudentGurdianException)
            {
                throw CreateAndLogValidationException(invalidStudentGurdianException);
            }
            catch(AlreadyPrimaryStudentGuardianExistsException alreadyPrimaryStudentGurdianExistsException)
            {
                throw CreateAndLogValidationException(alreadyPrimaryStudentGurdianExistsException);
            }
        }

        private StudentGuardianProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingValidationException);

            return studentGuardianProcessingValidationException;
        }
    }
}
