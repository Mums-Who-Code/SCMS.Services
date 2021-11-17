// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;
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
            catch (AlreadyPrimaryStudentGuardianExistsException alreadyPrimaryStudentGurdianExistsException)
            {
                throw CreateAndLogValidationException(alreadyPrimaryStudentGurdianExistsException);
            }
            catch (StudentGuardianDependencyException studentGuardianDependencyException)
            {
                throw CreateAndLogDependencyException(studentGuardianDependencyException);
            }
            catch (StudentGuardianServiceException studentGuardianServiceException)
            {
                throw CreateAndLogDependencyException(studentGuardianServiceException);
            }
            catch (Exception exception)
            {
                var failedStudentGuardianProcessingServiceException =
                    new FailedStudentGuardianProcessingServiceException(exception);

                throw CreateAndLogServiceException(failedStudentGuardianProcessingServiceException);
            }
        }

        private StudentGuardianProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentGuardianProcessingValidationException =
                new StudentGuardianProcessingValidationException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingValidationException);

            return studentGuardianProcessingValidationException;
        }

        private StudentGuardianProcessingDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var studentGuardianProcessingDependencyException =
                new StudentGuardianProcessingDependencyException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingDependencyException);

            return studentGuardianProcessingDependencyException;
        }

        private StudentGuardianProcessingServiceException CreateAndLogServiceException(Xeption exception)
        {
            var studentGuardianProcessingServiceException =
                new StudentGuardianProcessingServiceException(exception);

            this.loggingBroker.LogError(studentGuardianProcessingServiceException);

            return studentGuardianProcessingServiceException;
        }
    }
}
