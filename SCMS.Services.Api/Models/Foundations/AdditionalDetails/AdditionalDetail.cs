// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Models.Foundations.Users;

namespace SCMS.Services.Api.Models.Foundations.AdditionalDetails
{
    public class AdditionalDetail
    {
        public Guid Id { get; set; }

        public string Notes { get; set; }

        public int FideId { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }
    }
}