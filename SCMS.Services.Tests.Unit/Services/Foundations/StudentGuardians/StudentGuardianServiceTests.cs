// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.DateTimes;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Brokers.Storages;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Foundations.StudentGuardians
{
    public partial class StudentGuardianServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentGuardianService studentGuardianService;

        public StudentGuardianServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentGuardianService = new StudentGuardianService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentGuardian CreateRandomStudentGuardian() =>
            CreateStudentGuardianFiller().Create();

        private static Filler<StudentGuardian> CreateStudentGuardianFiller()
        {
            var filler = new Filler<StudentGuardian>();
            DateTimeOffset date = GetRandomDateTime();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
