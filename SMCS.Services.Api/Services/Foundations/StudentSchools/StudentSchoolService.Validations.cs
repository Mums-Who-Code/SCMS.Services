// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SMCS.Services.Api.Models.Foundations.StudentSchools;
using SMCS.Services.Api.Models.Foundations.StudentSchools.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.StudentSchools
{
    public partial class StudentSchoolService
    {
        private static void ValidateStudentSchool(StudentSchool studentSchool)
        {
            ValidationStudentSchoolIsNull(studentSchool);

            Validate(
                (Rule: IsInvalid(studentSchool.Id), Parameter: nameof(StudentSchool.Id)),
                (Rule: IsInvalid(studentSchool.StudentId), Parameter: nameof(StudentSchool.StudentId)),
                (Rule: IsInvalid(studentSchool.SchoolId), Parameter: nameof(StudentSchool.SchoolId)));
        }

        private static void ValidationStudentSchoolIsNull(StudentSchool studentSchool)
        {
            if (studentSchool is null)
            {
                throw new NullStudentSchoolException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentSchoolException = new InvalidStudentSchoolException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentSchoolException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentSchoolException.ThrowIfContainsErrors();
        }
    }
}
