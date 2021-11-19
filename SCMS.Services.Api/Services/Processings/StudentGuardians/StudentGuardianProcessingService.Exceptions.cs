// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService
    {
        private delegate ValueTask<StudentGuardian> ReturningStudentGuardianFunction();

        private async ValueTask<StudentGuardian> TryCatch(ReturningStudentGuardianFunction
            returningStudentGuardianFunction)
        {
            try
            {
                return await returningStudentGuardianFunction();
            }
            catch (NullStudentGuardianProcessingException nullStudentGuardianProcessingException)
            {
                throw CreateAndLogValidationException(nullStudentGuardianProcessingException);
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
