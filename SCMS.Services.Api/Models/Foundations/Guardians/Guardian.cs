// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SCMS.Services.Api.Models.Foundations.Emails;
using SCMS.Services.Api.Models.Foundations.Occupations;
using SCMS.Services.Api.Models.Foundations.Phones;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.Users;

namespace SCMS.Services.Api.Models.Foundations.Guardians
{
    public class Guardian
    {
        public Guid Id { get; set; }
        public Title Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        [JsonIgnore]
        public IEnumerable<StudentGuardian> RegisteredStudents { get; set; }

        public Phone RegisteredPhone { get; set; }
        public Email RegisteredEmail { get; set; }
        public Occupation RegisteredOccupation { get; set; }
    }
}
