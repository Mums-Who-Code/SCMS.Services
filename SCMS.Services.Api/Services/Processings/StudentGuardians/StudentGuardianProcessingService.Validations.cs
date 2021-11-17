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
        private static void ValidateIds(Guid studentId, Guid guardianId)
        {
            Validate(
                (Rule: IsInvalid(studentId), Parameter: nameof(StudentGuardian.StudentId)),
                (Rule: IsInvalid(guardianId), Parameter: nameof(StudentGuardian.GuardianId))
            );
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
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
