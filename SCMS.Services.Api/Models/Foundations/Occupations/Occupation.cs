// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Users;

namespace SCMS.Services.Api.Models.Foundations.Occupations
{
    public class Occupation
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid GuardianId { get; set; }
        public Guardian Guardian { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }
    }
}
