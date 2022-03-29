// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Agreements;
using SCMS.Services.Api.Models.Foundations.Users;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCMS.Services.Api.Models.Foundations.TermsAndConditions
{
    public class TermsAndCondition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public TermsAndConditionType Type { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        [JsonIgnore]
        public IEnumerable<Agreement> TermsAndConditiondAgreements { get; set; }
    }
}
