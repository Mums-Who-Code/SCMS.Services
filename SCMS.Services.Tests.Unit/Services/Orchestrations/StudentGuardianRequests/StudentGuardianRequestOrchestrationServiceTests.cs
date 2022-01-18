// -----------------------------------------------------------------------
// Copyright (c) Signature Chess Club & MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SCMS.Services.Api.Brokers.Loggings;
using SCMS.Services.Api.Models.Foundations.StudentGuardians;
using SCMS.Services.Api.Models.Processings.GuardianRequests;
using SCMS.Services.Api.Models.Processings.GuardianRequests.Exceptions;
using SCMS.Services.Api.Models.Processings.StudentGuardians.Exceptions;
using SCMS.Services.Api.Models.Processings.Students.Exceptions;
using SCMS.Services.Api.Services.Orchestrations.StudentGuardianRequests;
using SCMS.Services.Api.Services.Processings.GuardianRequests;
using SCMS.Services.Api.Services.Processings.StudentGuardians;
using SCMS.Services.Api.Services.Processings.Students;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace SCMS.Services.Tests.Unit.Services.Orchestrations.StudentGuardianRequests
{
    public partial class StudentGuardianRequestOrchestrationServiceTests
    {
        private readonly Mock<IStudentProcessingService> studentProcessingServiceMock;
        private readonly Mock<IGuardianRequestProcessingService> guardianRequestProcessingServiceMock;
        private readonly Mock<IStudentGuardianProcessingService> studentGuardianProcessingServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IStudentGuardianRequestOrchestrationService studentGuardianRequestOrchestrationService;
        private readonly ICompareLogic compareLogic;

        public StudentGuardianRequestOrchestrationServiceTests()
        {
            this.studentProcessingServiceMock = new Mock<IStudentProcessingService>();
            this.guardianRequestProcessingServiceMock = new Mock<IGuardianRequestProcessingService>();
            this.studentGuardianProcessingServiceMock = new Mock<IStudentGuardianProcessingService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.studentGuardianRequestOrchestrationService = new StudentGuardianRequestOrchestrationService(
                studentProcessingService: this.studentProcessingServiceMock.Object,
                guardianRequestProcessingService: this.guardianRequestProcessingServiceMock.Object,
                studentGuardianProcessingService: this.studentGuardianProcessingServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData DependencyValidationExceptions()
        {
            var someException = new Xeption();

            return new TheoryData<Xeption>()
            {
                new StudentProcessingValidationException(someException),
                new StudentProcessingDependencyValidationException(someException),
                new GuardianRequestProcessingValidationException(someException),
                new GuardianRequestProcessingDependencyValidationException(someException),
                new StudentGuardianProcessingValidationException(someException),
                new StudentGuardianProcessingDependencyValidationException(someException)
            };
        }

        public static TheoryData DependencyExceptions()
        {
            var someException = new Xeption();

            return new TheoryData<Xeption>()
            {
                new StudentProcessingDependencyException(someException),
                new StudentProcessingServiceException(someException),
                new GuardianRequestProcessingDependencyException(someException),
                new GuardianRequestProcessingServiceException(someException),
                new StudentGuardianProcessingDependencyException(someException),
                new StudentGuardianProcessingServiceException(someException)
            };
        }

        private Expression<Func<StudentGuardian, bool>> SameStudentGuardianAs(
            StudentGuardian expectedStudentGuardian)
        {
            return actualStudentGuardian =>
                this.compareLogic.Compare(expectedStudentGuardian, actualStudentGuardian)
                    .AreEqual;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
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

        private GuardianRequest CreateRandomGuardianRequest() =>
            CreateGuardianRequestFiller().Create();

        private Filler<StudentGuardian> CreateStudentGuardianFiller()
        {
            var filler = new Filler<StudentGuardian>();

            filler.Setup()
                .OnProperty(studentGuardian => studentGuardian.Guardian).IgnoreIt()
                .OnProperty(studentGuardian => studentGuardian.Student).IgnoreIt()
                .OnProperty(studentGuardian => studentGuardian.CreatedByUser).IgnoreIt()
                .OnProperty(studentGuardian => studentGuardian.UpdatedByUser).IgnoreIt()
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
