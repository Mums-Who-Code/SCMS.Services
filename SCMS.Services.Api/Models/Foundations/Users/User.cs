// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SCMS.Services.Api.Models.Foundations.Guardians;
using SCMS.Services.Api.Models.Foundations.Schools;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Foundations.Students;

namespace SCMS.Services.Api.Models.Foundations.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public IEnumerable<Student> CreatedStudents { get; set; }

        [JsonIgnore]
        public IEnumerable<Student> UpdatedStudents { get; set; }

        [JsonIgnore]
        public IEnumerable<School> CreatedSchools { get; set; }

        [JsonIgnore]
        public IEnumerable<School> UpdatedSchools { get; set; }

        [JsonIgnore]
        public IEnumerable<Guardian> CreatedGuardians { get; set; }

        [JsonIgnore]
        public IEnumerable<Guardian> UpdatedGuardians { get; set; }

        [JsonIgnore]
        public IEnumerable<StudentGuardian> CreatedStudentGuardians { get; set; }

        [JsonIgnore]
        public IEnumerable<StudentGuardian> UpdatedStudentGuardians { get; set; }
    }
}
