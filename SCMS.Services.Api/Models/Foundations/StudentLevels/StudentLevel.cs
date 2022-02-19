// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using SCMS.Services.Api.Models.Foundations.Users;

namespace SCMS.Services.Api.Models.Foundations.StudentLevels
{
    public class StudentLevel
    {
        public string Name { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; } 
    }
}
