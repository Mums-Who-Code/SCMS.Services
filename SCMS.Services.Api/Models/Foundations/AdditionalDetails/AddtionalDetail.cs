// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Models.Foundations.AdditionalDetails
{
    public class AdditionalDetail
    {
        public Guid Id { get; set; }

        public string Notes { get; set; }

        public int Fide { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public Guid UpdatedBy { get; set; }

    }
}
