// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.Students;
using SCMS.Services.Api.Services.Foundations.Students;
using SCMS.Services.Api.Services.Processings.Students;
using Tynamix.ObjectFiller;
using Xeptions;

namespace SCMS.Services.Tests.Unit.Services.Processings.Students
{
    public partial class StudentProcessingServiceTests
    {
        private readonly Mock<IStudentService> studentServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentProcessingService studentProcessingService;

        public StudentProcessingServiceTests()
        {
            this.studentServiceMock = new Mock<IStudentService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentProcessingService = new StudentProcessingService(
                studentService: this.studentServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static DateTimeOffset GetRandomDateTime() =>
           new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Student CreateRandomStudent() =>
            CreateStudentFiller(dateTime: GetRandomDateTime()).Create();

        private static Filler<Student> CreateStudentFiller(DateTimeOffset dateTime)
        {
            var filler = new Filler<Student>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dateTime)
                .OnProperty(student => student.CreatedByUser).IgnoreIt()
                .OnProperty(student => student.UpdatedByUser).IgnoreIt()
                .OnProperty(student => student.RegisteredGuardians).IgnoreIt()
                .OnProperty(student => student.EnrolledSchool).IgnoreIt();

            return filler;
        }
    }
}
