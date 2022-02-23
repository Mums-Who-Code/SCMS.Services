// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using SCMS.Services.Api.Models.Foundations.StudentLevels;
using SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions;
using Xeptions;

namespace SCMS.Services.Api.Services.Foundations.StudentLevels
{
   
    public partial class StudentLevelService
    {
        private delegate ValueTask<StudentLevel> ReturningStudentLevelFunction();

        private async ValueTask<StudentLevel> TryCatch(ReturningStudentLevelFunction returningStudentLevelFunction)
        {
            try
            {
                return await returningStudentLevelFunction();
            }
            catch (NullStudentLevelException nullStudentLevelException)
            {
                throw CreateAndLogValidationException(nullStudentLevelException);
            }
        }

        private Xeption CreateAndLogValidationException(Xeption exception)
        {
            var studentLevelValidationException = new StudentLevelValidationException(exception);
            this.loggingBroker.LogError(studentLevelValidationException);

            return studentLevelValidationException;
        }
    }
}
