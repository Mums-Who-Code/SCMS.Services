// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SMCS.Services.Api.Brokers.DateTimes;
using SMCS.Services.Api.Brokers.Loggings;
using SMCS.Services.Api.Brokers.Storages;
using SMCS.Services.Api.Models.Foundations.Students;
using SMCS.Services.Api.Services.Foundations.Students;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Foundations.Students
{
    public partial class StudentServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> logginBrokerMock;
        private readonly IStudentService studentService;

        public StudentServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.logginBrokerMock = new Mock<ILoggingBroker>();

            this.studentService = new StudentService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.logginBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Student CreateRandomStudent() =>
            CreateStudentFiller().Create();

        private static Filler<Student> CreateStudentFiller()
        {
            var filler = new Filler<Student>();
            Guid userId = Guid.NewGuid();
            DateTimeOffset date = GetRandomDateTime();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
