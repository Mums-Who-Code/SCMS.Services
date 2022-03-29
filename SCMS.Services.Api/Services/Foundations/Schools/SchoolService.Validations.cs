// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.Schools.Exceptions;
using System;

namespace SCMS.Services.Api.Services.Foundations.Schools
{
    public partial class SchoolService
    {
        private void ValidateSchool(School school)
        {
            ValidateSchoolIsNotNull(school);

            Validate(
                (Rule: IsInvalid(school.Id), Parameter: nameof(School.Id)),
                (Rule: IsInvalid(text: school.Name), Parameter: nameof(School.Name)),
                (Rule: IsInvalid(school.CreatedDate), Parameter: nameof(School.CreatedDate)),
                (Rule: IsInvalid(school.UpdatedDate), Parameter: nameof(School.UpdatedDate)),
                (Rule: IsInvalid(school.CreatedBy), Parameter: nameof(School.CreatedBy)),
                (Rule: IsInvalid(school.UpdatedBy), Parameter: nameof(School.UpdatedBy)),

                (Rule: IsNotSameAs(
                    firstDate: school.UpdatedDate,
                    secondDate: school.CreatedDate,
                    secondParameterName: nameof(School.CreatedDate)),
                Parameter: nameof(School.UpdatedDate)),

                (Rule: IsNotSameAs(
                    firstId: school.UpdatedBy,
                    secondId: school.CreatedBy,
                    secondParameterName: nameof(School.CreatedBy)),
                Parameter: nameof(School.UpdatedBy)),

                (Rule: IsNotRecent(school.CreatedDate), Parameter: nameof(School.CreatedDate)));
        }

        private static void ValidateSchoolIsNotNull(School school)
        {
            if (school is null)
            {
                throw new NullSchoolException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required."
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
            var invalidSchoolException = new InvalidSchoolException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSchoolException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSchoolException.ThrowIfContainsErrors();
        }
    }
}
