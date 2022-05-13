// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Models.Foundations.Agreements.Exceptions;

namespace SCMS.Services.Api.Services.Foundations.Agreements
{
    public partial class AgreementService
    {
        private static void ValidateInput(Agreement agreement)
        {
            ValidateArtistIsNotNull(agreement);

            Validate(
              (Rule: IsInvalid(agreement.Id), Parameter: nameof(Agreement.Id)),
              (Rule: IsInvalid(agreement.Status), Parameter: nameof(Agreement.Status)),
              (Rule: IsInvalid(id: agreement.CreatedBy), Parameter: nameof(Agreement.CreatedBy)),
              (Rule: IsInvalid(id: agreement.UpdatedBy), Parameter: nameof(Agreement.UpdatedBy)),
              (Rule: IsInvalid(agreement.CreatedDate), Parameter: nameof(Agreement.CreatedDate)),
              (Rule: IsInvalid(agreement.UpdatedDate), Parameter: nameof(Agreement.UpdatedDate)));
        }

        private static void ValidateArtistIsNotNull(Agreement agreement)
        {
            if (agreement == null)
            {
                throw new NullAgreementException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required."
        };

        private static dynamic IsInvalid(AgreementStatus status) => new
        {
            Condition = status != AgreementStatus.Accepted,
            Message = "Value is invalid."
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required."
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAgreementException = new InvalidAgreementException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAgreementException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAgreementException.ThrowIfContainsErrors();
        }
    }
}
