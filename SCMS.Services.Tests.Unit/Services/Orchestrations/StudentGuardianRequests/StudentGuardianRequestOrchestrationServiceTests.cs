// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests;
using SCMS.Services.Api.Services.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using SCMS.Services.Api.Services.Processings.Students;
using Tynamix.ObjectFiller;

namespace SCMS.Services.Tests.Unit.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationServiceTests
    {
        private readonly Mock<IStudentProcessingService> studentProcessingServiceMock;
        private readonly Mock<IGuardianRequestProcessingService> guardianRequestProcessingServiceMock;
        private readonly Mock<IStudentGuardianProcessingService> studentGuardianProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentGuardianRequestOrchestrationService studentGuardianRequestOrchestrationService;

        public StudentGuardianRequestOrchestrationServiceTests()
        {
            this.studentProcessingServiceMock = new Mock<IStudentProcessingService>();
            this.guardianRequestProcessingServiceMock = new Mock<IGuardianRequestProcessingService>();
            this.studentGuardianProcessingServiceMock = new Mock<IStudentGuardianProcessingService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.studentGuardianRequestOrchestrationService = new StudentGuardianRequestOrchestrationService(
                studentProcessingService: this.studentProcessingServiceMock.Object,
                guardianRequestProcessingService: this.guardianRequestProcessingServiceMock.Object,
                studentGuardianRequestProcessingService: this.studentGuardianProcessingServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private StudentGuardian CreateRandomStudentGuardian() =>
            CreateStudentGuardianFiller().Create();

        private GuardianRequest CreateRandomGuardianRequestWith(StudentGuardian studentGuardian)
        {
            GuardianRequest filler = CreateGuardianRequestFiller().Create();
            filler.StudentId = studentGuardian.StudentId;
            filler.Id = studentGuardian.GuardianId;
            filler.Relationship = (GuardianRequestRelationship)studentGuardian.Relation;
            filler.ContactLevel = (GuardianRequestContactLevel)studentGuardian.Level;
            filler.CreatedDate = studentGuardian.CreatedDate;
            filler.UpdatedBy = studentGuardian.UpdatedBy;
            filler.UpdatedDate = studentGuardian.UpdatedDate;
            filler.CreatedBy = studentGuardian.CreatedBy;

            return filler;
        }

        private Filler<StudentGuardian> CreateStudentGuardianFiller()
        {
            var filler = new Filler<StudentGuardian>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler;
        }

        private Filler<GuardianRequest> CreateGuardianRequestFiller()
        {
            var filler = new Filler<GuardianRequest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTime());

            return filler;
        }
    }
}
