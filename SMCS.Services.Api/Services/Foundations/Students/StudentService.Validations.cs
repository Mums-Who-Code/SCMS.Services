// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SMCS.Services.Api.Models.Foundations.Students;
using SMCS.Services.Api.Models.Foundations.Students.Exceptions;

namespace SMCS.Services.Api.Services.Foundations.Students
{
    public partial class StudentService
    {

        private void ValidateStudentOnAdd(Student student)
        {
            ValidateInput(student);

            Validate(
                (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalid(text: student.FirstName), Parameter: nameof(Student.FirstName)),
                (Rule: IsInvalid(text: student.LastName), Parameter: nameof(Student.LastName)));
        }

        private void ValidateInput(Student student)
        {
            if (student == null)
            {
                throw new NullStudentException();
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

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidStudentException = new InvalidStudentException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentException.ThrowIfContainsErrors();
        }
    }
}
