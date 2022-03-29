// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Students.Exceptions;
using System;

namespace SCMS.Services.Api.Services.Foundations.Students
{
    public partial class StudentService
    {
        private void ValidateStudentOnAdd(Student student)
        {
            ValidateInput(student);

            Validate(
                (Rule: IsInvalid(student.Id), Parameter: nameof(Student.Id)),
                (Rule: IsInvalid(text: student.FirstName), Parameter: nameof(Student.FirstName)),
                (Rule: IsInvalid(text: student.LastName), Parameter: nameof(Student.LastName)),
                (Rule: IsInvalid(date: student.DateOfBirth), Parameter: nameof(Student.DateOfBirth)),
                (Rule: IsInvalid(student.Status), Parameter: nameof(Student.Status)),
                (Rule: IsInvalid(student.SchoolId), Parameter: nameof(Student.SchoolId)),
                (Rule: IsInvalid(student.CreatedDate), Parameter: nameof(Student.CreatedDate)),
                (Rule: IsInvalid(id: student.CreatedBy), Parameter: nameof(Student.CreatedBy)),

                (Rule: IsNotSame(
                    firstDate: student.UpdatedDate,
                    secondDate: student.CreatedDate,
                    secondDateName: nameof(Student.CreatedDate)),
                Parameter: nameof(Student.UpdatedDate)),

                 (Rule: IsNotSame(
                    firstId: student.UpdatedBy,
                    secondId: student.CreatedBy,
                    secondIdName: nameof(Student.CreatedBy)),
                Parameter: nameof(Student.UpdatedBy)),

                (Rule: IsNotRecent(student.CreatedDate), Parameter: nameof(Student.CreatedDate))
            );
        }

        private void ValidateStudentId(Guid studentId) =>
            Validate((Rule: IsInvalid(studentId), Parameter: nameof(Student.Id)));

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

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static dynamic IsInvalid(StudentStatus status) => new
        {
            Condition = status != StudentStatus.Active,
            Message = "Value is invalid."
        };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not same as {secondDateName}."
            };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not same as {secondIdName}."
            };

        private dynamic IsNotRecent(DateTimeOffset dateTimeOffset) => new
        {
            Condition = IsDateNotRecent(dateTimeOffset),
            Message = "Date is not recent."
        };

        private bool IsDateNotRecent(DateTimeOffset dateTime)
        {
            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTime();
            int oneMinute = 1;
            TimeSpan difference = now.Subtract(dateTime);

            return Math.Abs(difference.TotalMinutes) > oneMinute;
        }

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
