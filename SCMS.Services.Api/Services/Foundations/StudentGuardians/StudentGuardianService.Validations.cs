// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.StudentGuardians.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianService : IStudentGuardianService
    {
        private static void ValidateStudentGuardian(StudentGuardian studentGuardian)
        {
            ValidateStudentGuardianIsNull(studentGuardian);

            Validate(
                (Rule: IsInvalid(studentGuardian.StudentId), Parameter: nameof(StudentGuardian.StudentId)),
                (Rule: IsInvalid(studentGuardian.GuardianId), Parameter: nameof(StudentGuardian.GuardianId)),
                (Rule: IsInvalid(studentGuardian.Relation), Parameter: nameof(StudentGuardian.Relation)),
                (Rule: IsInvalid(studentGuardian.Level), Parameter: nameof(StudentGuardian.Level)),
                (Rule: IsInvalid(studentGuardian.CreatedBy), Parameter: nameof(StudentGuardian.CreatedBy)),
                (Rule: IsInvalid(studentGuardian.CreatedDate), Parameter: nameof(StudentGuardian.CreatedDate)),

                (Rule: IsInvalid(
                    firstDate: studentGuardian.CreatedDate,
                    secondDate: studentGuardian.UpdatedDate,
                    secondDateName: nameof(StudentGuardian.UpdatedDate)),
                Parameter: nameof(StudentGuardian.CreatedDate)),

                (Rule: IsInvalid(
                    firstId: studentGuardian.CreatedBy,
                    secondId: studentGuardian.UpdatedBy,
                    secondIdName: nameof(StudentGuardian.UpdatedBy)),
                Parameter: nameof(StudentGuardian.CreatedBy))
            );
        }

        private static void ValidateStudentGuardianIsNull(StudentGuardian studentGuardian)
        {
            if (studentGuardian is not null)
            {
                return;
            }
            throw new NullStudentGuardianException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required."
        };

        private static dynamic IsInvalid<T>(T enumValue) => new
        {
            Condition = Enum.IsDefined(typeof(T), enumValue) is false,
            Message = "Value is not recognized."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsInvalid(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}."
            };

        private static dynamic IsInvalid(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not same as {secondIdName}."
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentGuardianException = new InvalidStudentGuardianException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentGuardianException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentGuardianException.ThrowIfContainsErrors();
        }
    }
}
