// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;

namespace SCMS.Services.Api.Services.Processings.Students
{
    public partial class StudentProcessingService : IStudentProcessingService
    {
        private static void Validate(Guid studentId)
        {
            Validate(
                (Rule: IsInvalid(studentId), Parameter: nameof(Student.Id)));
        }

        private static void ValidateReturningStudent(Student student, Guid studentId)
        {
            if (student == null)
            {
                throw new NotFoundStudentProcessingException(studentId);
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentProcessingException = new InvalidStudentProcessingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentProcessingException.AddData(
                        key: parameter,
                        values: rule.Message);
                }
            }

            invalidStudentProcessingException.ThrowIfContainsErrors();
        }
    }
}
