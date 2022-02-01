// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Orchestrations.StudentGuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.GuardianRequests;

namespace SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationService
    {
        private void ValidateStudentGuardianRequest(GuardianRequest guardianRequest)
        {
            ValidateStudentGuardianRequestIsNull(guardianRequest);

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

        private static void ValidateStudentGuardianRequestIsNull(GuardianRequest guardianRequest)
        {
            if (guardianRequest == null)
            {
                throw new NullStudentGuardianRequestOrchestrationException();
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
            var invalidStudentGuardianRequestOrchestrationException =
                new InvalidStudentGuardianRequestOrchestrationException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidStudentGuardianRequestOrchestrationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidStudentGuardianRequestOrchestrationException.ThrowIfContainsErrors();
        }
    }
}
