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
        private void ValidateStudentSchool(StudentSchool studentSchool)
        {
            ValidationStudentSchoolIsNull(studentSchool);

            Validate(
                (Rule: IsInvalid(studentSchool.StudentId), Parameter: nameof(StudentSchool.StudentId)),
                (Rule: IsInvalid(studentSchool.SchoolId), Parameter: nameof(StudentSchool.SchoolId)),
                (Rule: IsInvalid(studentSchool.CreatedDate), Parameter: nameof(StudentSchool.CreatedDate)),
                (Rule: IsInvalid(studentSchool.CreatedBy), Parameter: nameof(StudentSchool.CreatedBy)),

                (Rule: IsNotSameAs(
                    firstDate: studentSchool.UpdatedDate,
                    secondDate: studentSchool.CreatedDate,
                    secondParameterName: nameof(StudentSchool.CreatedDate)),
                Parameter: nameof(StudentSchool.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstId: studentSchool.UpdatedBy,
                    secondId: studentSchool.CreatedBy,
                    secondParameterName: nameof(StudentSchool.CreatedBy)),
                Parameter: nameof(studentSchool.UpdatedBy)),

                (Rule: IsNotRecent(studentSchool.CreatedDate), Parameter: nameof(StudentSchool.CreatedDate)));
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

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsNotSameAs(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondParameterName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondParameterName}"
            };

        private static dynamic IsNotSameAs(
            Guid firstId,
            Guid secondId,
            string secondParameterName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not same as {secondParameterName}"
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent"
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTime();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }


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
