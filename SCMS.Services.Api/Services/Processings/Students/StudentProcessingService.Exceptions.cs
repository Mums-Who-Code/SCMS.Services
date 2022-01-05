// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public partial class StudentProcessingService : IStudentProcessingService
    {
        private delegate ValueTask<Student> ReturningStudentFunction();

        private async ValueTask<Student> TryCatch(ReturningStudentFunction returningStudentFunction)
        {
            try
            {
                return await returningStudentFunction();
            }
            catch (InvalidStudentProcessingException invalidStudentProcessingException)
            {
                throw CreateAndLogValidationException(invalidStudentProcessingException);
            }
        }

        private StudentProcessingValidationException CreateAndLogValidationException(Xeption exception)
        {
            var studentProcessingValidationException = new StudentProcessingValidationException(exception);
            this.loggingBroker.LogError(studentProcessingValidationException);

            throw studentProcessingValidationException;
        }
    }
}
