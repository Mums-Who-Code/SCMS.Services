// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using Newtonsoft.Json;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.Users;
using System;
using System.Collections.Generic;

namespace SCMS.Services.Api.Models.Foundations.Guardians
{
    public class Guardian
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string ContactNumber { get; set; }
        public string Occupation { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        [JsonIgnore]
        public IEnumerable<StudentGuardian> RegisteredStudents { get; set; }
    }
}
