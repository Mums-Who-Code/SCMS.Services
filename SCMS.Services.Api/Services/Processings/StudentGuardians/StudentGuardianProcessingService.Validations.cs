// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;

namespace SCMS.Services.Api.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingService
    {
        private static void ValidateStudentGuardian(StudentGuardian studentGuardian)
        {
            ValidateStudentGuardianIsNull(studentGuardian);

            Validate(
                (Rule: IsInvalid(studentGuardian.StudentId), Parameter: nameof(StudentGuardian.StudentId)),
                (Rule: IsInvalid(studentGuardian.Level), Parameter: nameof(StudentGuardian.Level))
            );
        }

        private static void ValidateStudentGuardianIsNull(StudentGuardian studentGuardian)
        {
            if (studentGuardian == null)
            {
                throw new NullStudentGuardianProcessingException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid<T>(T enumValue) => new
        {
            Condition = Enum.IsDefined(typeof(T), enumValue) is false,
            Message = "Value is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentGuardianProcessingException =
                new InvalidStudentGuardianProcessingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentGuardianProcessingException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentGuardianProcessingException.ThrowIfContainsErrors();
        }
    }
}
