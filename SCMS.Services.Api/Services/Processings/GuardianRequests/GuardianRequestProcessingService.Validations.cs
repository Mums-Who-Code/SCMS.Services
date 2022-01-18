// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;

namespace SCMS.Services.Api.Services.Processings.GuardianRequests
{
    public partial class GuardianRequestProcessingService
    {
        private void ValidateGuardianRequest(GuardianRequest guardianRequest)
        {
            ValidateGuardianRequestIsNull(guardianRequest);

            Validate(
                (Rule: IsInvalid(guardianRequest.Id), Parameter: nameof(GuardianRequest.Id)),
                (Rule: IsInvalid(text: guardianRequest.FirstName), Parameter: nameof(GuardianRequest.FirstName)),
                (Rule: IsInvalid(text: guardianRequest.LastName), Parameter: nameof(GuardianRequest.LastName)),
                (Rule: IsInvalid(text: guardianRequest.EmailId), Parameter: nameof(GuardianRequest.EmailId)),
                (Rule: IsInvalid(text: guardianRequest.CountryCode), Parameter: nameof(GuardianRequest.CountryCode)),
                (Rule: IsInvalid(text: guardianRequest.ContactNumber), Parameter: nameof(GuardianRequest.ContactNumber)),
                (Rule: IsInvalid(text: guardianRequest.Occupation), Parameter: nameof(GuardianRequest.Occupation)),
                (Rule: IsInvalid(guardianRequest.StudentId), Parameter: nameof(GuardianRequest.StudentId)),
                (Rule: IsInvalid(guardianRequest.CreatedDate), Parameter: nameof(GuardianRequest.CreatedDate)),
                (Rule: IsInvalid(guardianRequest.UpdatedDate), Parameter: nameof(GuardianRequest.UpdatedDate)),
                (Rule: IsInvalid(id: guardianRequest.CreatedBy), Parameter: nameof(GuardianRequest.CreatedBy)),
                (Rule: IsInvalid(id: guardianRequest.UpdatedBy), Parameter: nameof(GuardianRequest.UpdatedBy)));
        }

        private static void ValidateGuardianRequestIsNull(GuardianRequest guardianRequest)
        {
            if (guardianRequest == null)
            {
                throw new NullGuardianRequestProcessingException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
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

        public void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidGuardianRequestProcessingException =
                new InvalidGuardianRequestProcessingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidGuardianRequestProcessingException.AddData(
                        key: parameter,
                        values: rule.Message);
                }
            }

            invalidGuardianRequestProcessingException.ThrowIfContainsErrors();
        }
    }
}
