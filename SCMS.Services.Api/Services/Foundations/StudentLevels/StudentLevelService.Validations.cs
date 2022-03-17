// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.StudentLevels;
using SCMS.Services.Api.Models.Foundations.StudentLevels.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.StudentLevels
{
    public partial class StudentLevelService
    {
        private void ValidateStudentLevel(StudentLevel studentLevel)
        {
            ValidateStudentLevelIsNull(studentLevel);

            Validate(
                (Rule: IsInvalid(studentLevel.Name), Parameter: nameof(StudentLevel.Name)),
                (Rule: IsInvalid(studentLevel.CreatedDate), Parameter: nameof(StudentLevel.CreatedDate)),
                (Rule: IsInvalid(studentLevel.UpdatedDate), Parameter: nameof(StudentLevel.UpdatedDate)),
                (Rule: IsInvalid(id: studentLevel.CreatedBy), Parameter: nameof(StudentLevel.CreatedBy)),
                (Rule: IsInvalid(id: studentLevel.UpdatedBy), Parameter: nameof(StudentLevel.UpdatedBy)),

                (Rule: IsNotSame(
                    firstDate: studentLevel.UpdatedDate,
                    secondDate: studentLevel.CreatedDate,
                    secondDateName: nameof(studentLevel.CreatedDate)),
               Parameter: nameof(StudentLevel.UpdatedDate)),

                (Rule: IsNotSame(
                    firstId: studentLevel.UpdatedBy,
                    secondId: studentLevel.CreatedBy,
                    secondIdName: nameof(StudentLevel.CreatedBy)),
                Parameter: nameof(StudentLevel.UpdatedBy)));
        }

        private static void ValidateStudentLevelIsNull(StudentLevel studentLevel)
        {
            if (studentLevel == null)
            {
                throw new NullStudentLevelException();
            }
        }
                    
        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsInvalid(string name) => new
        {
            Condition = string.IsNullOrWhiteSpace(name),
            Message = "Name is required."
        };

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}."
            };

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsNotRecent(date),
            Message = "Date is not recent."
        };

        private static dynamic IsNotSame(
           Guid firstId,
           Guid secondId,
           string secondIdName) => new
           {
               Condition = firstId != secondId,
               Message = $"Id is not same as {secondIdName}."
           };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentLevelException = new InvalidStudentLevelException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentLevelException.UpsertDataList(
                    key: parameter,
                    value: rule.Message);
                }
            }

                invalidStudentLevelException.ThrowIfContainsErrors();
        }

    }

}

