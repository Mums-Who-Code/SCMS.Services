// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Text.Json.Serialization;
using SCMS.Services.Api.Models.Foundations.StudentSchools;
using SCMS.Services.Api.Models.Foundations.Users;

namespace SCMS.Services.Api.Models.Foundations.Students
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public StudentStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }

        public Guid CreatedBy { get; set; }
        public User CreatedByUser { get; set; }

        public Guid UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }

        [JsonIgnore]
        public StudentSchool EnrolledSchool { get; set; }
    }
}
