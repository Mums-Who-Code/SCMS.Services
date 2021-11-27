// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.Phones.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.Phones
{
    public partial class PhoneService
    {
        private void ValidatePhone(Phone phone)
        {
            ValidatePhoneIsNull(phone);

            Validate(
                (Rule: IsInvalid(phone.Id), Parameter: nameof(Phone.Id)),
                (Rule: IsInvalid(text: phone.CountryCode), Parameter: nameof(Phone.CountryCode)),
                (Rule: IsInvalidNumber(phone.Number), Parameter: nameof(Phone.Number)),
                (Rule: IsInvalid(phone.CreatedDate), Parameter: nameof(Phone.CreatedDate)),
                (Rule: IsInvalid(phone.UpdatedDate), Parameter: nameof(Phone.UpdatedDate)),
                (Rule: IsInvalid(id: phone.CreatedBy), Parameter: nameof(Phone.CreatedBy)),
                (Rule: IsInvalid(id: phone.UpdatedBy), Parameter: nameof(Phone.UpdatedBy)),

                (Rule: IsNotSame(
                    firstDate: phone.UpdatedDate,
                    secondDate: phone.CreatedDate,
                    secondDateName: nameof(Phone.CreatedDate)),
                Parameter: nameof(Phone.UpdatedDate)),

                (Rule: IsNotSame(
                    firstId: phone.UpdatedBy,
                    secondId: phone.CreatedBy,
                    secondIdName: nameof(Phone.CreatedBy)),
                Parameter: nameof(Phone.UpdatedBy)),

                (Rule: IsNotRecent(phone.CreatedDate), Parameter: nameof(Phone.CreatedDate)));
        }

        private static void ValidatePhoneIsNull(Phone phone)
        {
            if (phone == null)
            {
                throw new NullPhoneException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
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

        private dynamic IsNotRecent(DateTimeOffset date) => new
        {
            Condition = IsDateNotRecent(date),
            Message = "Date is not recent."
        };

        private bool IsDateNotRecent(DateTimeOffset date)
        {
            DateTimeOffset currentDateTime =
                this.dateTimeBroker.GetCurrentDateTime();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan oneMinute = TimeSpan.FromMinutes(1);

            return timeDifference.Duration() > oneMinute;
        }

        private static dynamic IsInvalidNumber(string number) => new
        {
            Condition = IsNotMatch(number),
            Message = "Text is invalid."
        };

        private static bool IsNotMatch(string number)
        {
            Regex regex = new Regex(@"^[0-9]{10}$");
            return !regex.IsMatch(number);
        }

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
                var invalidPhoneException = new InvalidPhoneException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidPhoneException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidPhoneException.ThrowIfContainsErrors();
        }
    }
}
