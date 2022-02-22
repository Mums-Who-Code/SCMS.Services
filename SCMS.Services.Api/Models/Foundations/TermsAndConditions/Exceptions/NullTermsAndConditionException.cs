// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Xeptions;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions.Exceptions
{
    public class NullTermsAndConditionException : Xeption
    {
        public NullTermsAndConditionException()
            : base(message: "Terms And Condition is null.")
        { }
    }
}
