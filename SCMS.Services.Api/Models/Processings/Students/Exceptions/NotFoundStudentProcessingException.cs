// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Xeptions;

namespace SCMS.Services.Api.Models.Processings.Students.Exceptions
{
    public class NotFoundStudentProcessingException : Xeption
    {
        public NotFoundStudentProcessingException(Guid id)
            : base(message: $"Student with id: {id} is not found.")
        { }
    }
}
