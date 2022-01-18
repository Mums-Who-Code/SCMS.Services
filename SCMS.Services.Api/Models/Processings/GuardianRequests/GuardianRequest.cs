// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;

namespace SCMS.Services.Api.Models.Processings.GuardianRequests
{
    public class GuardianRequest
    {
        public Guid Id { get; set; }
        public GuardianRequestTitle Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string CountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string Occupation { get; set; }
        public GuardianRequestContactLevel ContactLevel { get; set; }
        public GuardianRequestRelationship Relationship { get; set; }
        public Guid StudentId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
