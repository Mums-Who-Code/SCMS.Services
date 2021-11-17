// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Foundations.StudentGuardians;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Processings.StudentGuardians
{
    public partial class StudentGuardianProcessingTests
    {
        private readonly Mock<IStudentGuardianService> studentGuardianServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentGuardianProcessingService studentGuardianProcessingService;

        public StudentGuardianProcessingTests()
        {
            this.studentGuardianServiceMock = new Mock<IStudentGuardianService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentGuardianProcessingService = new StudentGuardianProcessingService(
                studentGuardianService: this.studentGuardianServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static DateTimeOffset GetRandomDateTime() =>
           new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static StudentGuardian CreateRandomStudentGuardian() =>
            CreateStudentGuardianFiller(dates: GetRandomDateTime()).Create();

        private static IQueryable<StudentGuardian> CreateRandomStudentGuardiansWithStudentGuardian(
            StudentGuardian studentGuardian)
        {
            List<StudentGuardian> studentGuardians = CreateRandomStudentGuardians().ToList();

            return studentGuardians.Append(studentGuardian).AsQueryable();
        }

        private static IQueryable<StudentGuardian> CreateRandomStudentGuardians()
        {
            return CreateStudentGuardianFiller(dates: GetRandomDateTime())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static Filler<StudentGuardian> CreateStudentGuardianFiller(DateTimeOffset dates)
        {
            var filler = new Filler<StudentGuardian>();
            Guid userId = Guid.NewGuid();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnType<Guid>().Use(userId);

            return filler;
        }
    }
}
