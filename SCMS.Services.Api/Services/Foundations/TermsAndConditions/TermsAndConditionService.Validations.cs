// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions;
using SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.TermsAndConditions
{
    public partial class TermsAndConditionService
    {
        private static void ValidateInput(TermsAndCondition termsAndCondition)
        {
            ValidateTermsAndConditionIsNotNull(termsAndCondition);

            Validate(
               (Rule: IsInvalid(termsAndCondition.Id), Parameter: nameof(TermsAndCondition.Id)),
               (Rule: IsInvalid(text: termsAndCondition.Name), Parameter: nameof(TermsAndCondition.Name)),
               (Rule: IsInvalidUrl(termsAndCondition.Url), Parameter: nameof(TermsAndCondition.Url)),
               (Rule: IsInvalid(termsAndCondition.Type), Parameter: nameof(TermsAndCondition.Type)),
               (Rule: IsInvalid(id: termsAndCondition.CreatedBy), Parameter: nameof(TermsAndCondition.CreatedBy)),
               (Rule: IsInvalid(id: termsAndCondition.UpdatedBy), Parameter: nameof(TermsAndCondition.UpdatedBy)),
               (Rule: IsInvalid(termsAndCondition.CreatedDate), Parameter: nameof(TermsAndCondition.CreatedDate)),
               (Rule: IsInvalid(termsAndCondition.UpdatedDate), Parameter: nameof(TermsAndCondition.UpdatedDate)),

               (Rule: IsNotSame(
                    firstDate: termsAndCondition.CreatedDate,
                    secondDate: termsAndCondition.UpdatedDate,
                    secondDateName: nameof(TermsAndCondition.UpdatedDate)),
                Parameter: nameof(TermsAndCondition.CreatedDate)),

               (Rule: IsNotSame(
                    firstId: termsAndCondition.CreatedBy,
                    secondId: termsAndCondition.UpdatedBy,
                    secondIdName: nameof(TermsAndCondition.UpdatedBy)),
                Parameter: nameof(TermsAndCondition.CreatedBy)));
        }

        private static void ValidateTermsAndConditionIsNotNull(TermsAndCondition termsAndCondition)
        {
            if (termsAndCondition == null)
            {
                throw new NullTermsAndConditionException();
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
            Message = "Text is invalid."
        };

        private static dynamic IsInvalidUrl(string url) => new
        {
            Condition = String.IsNullOrWhiteSpace(url) || IsInvalidUrlFormat(url),
            Message = "Text is invalid."
        };

        private static bool IsInvalidUrlFormat(string value) =>
            Uri.IsWellFormedUriString(value, UriKind.Absolute) is false;

        private static dynamic IsInvalid(TermsAndConditionType status) => new
        {
            Condition = status != TermsAndConditionType.Registration,
            Message = "Value is not recognized."
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
                Message = $"Id is not same as {secondIdName}"
            };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidTermsAndConditionException = new InvalidTermsAndConditionException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidTermsAndConditionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidTermsAndConditionException.ThrowIfContainsErrors();
        }
    }
}
