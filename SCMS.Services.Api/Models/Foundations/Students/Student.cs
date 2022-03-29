// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.Users;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SCMS.Services.Api.Models.Foundations.Students
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public StudentGender Gender { get; set; }
        public string FideId { get; set; }
        public string Notes { get; set; }
        public StudentStatus Status { get; set; }

        public Guid SchoolId { get; set; }
        public School EnrolledSchool { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        [JsonIgnore]
        public IEnumerable<StudentGuardian> RegisteredGuardians { get; set; }
    }
}
