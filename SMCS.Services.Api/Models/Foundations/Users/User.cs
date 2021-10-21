// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SMCS.Services.Api.Models.Foundations.Schools;
using SMCS.Services.Api.Models.Foundations.Students;

namespace SMCS.Services.Api.Models.Foundations.Users
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
    }
}
