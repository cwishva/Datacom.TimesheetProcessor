using Application;
using Application.Services;
using Application.Services.DTo;
using Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TimesheetProcessor.UnitTests.Application
{
    public class TimesheetProcessorTest
    {
        private readonly Mock<IDatacomService> _datacomService;

        public TimesheetProcessorTest()
        {
            this._datacomService = new Mock<IDatacomService>();

            this._datacomService
                .Setup(x => x.GetCompanies())
                .ReturnsAsync(new List<Company>
                {
                    new Company
                    { 
                        Name = "TestCompany",
                        Code = "TESTUnitTest", 
                        Id = "123456789"
                    }
                });

            this._datacomService
                .Setup(x => x.GetPaygroups("123456789"))
                .ReturnsAsync(new List<Paygroups>
                {
                    new Paygroups
                    { 
                        Id = "1234567890", 
                        Code = "TestPaygroups",
                        CompanyId = "123456789", 
                        Name = "TestPaygroups"
                    }
                });

            this._datacomService
                .Setup(x => x.GetPayruns(It.IsAny<string>()))
                .ReturnsAsync(new List<Payrun>
                {
                    new Payrun
                    {
                        Id = 123456789,
                        PayGroupId = "1234567890",
                        PayPeriod = new PayPeriod
                        {
                            EndDate = DateTime.Parse("2019-08-05"),
                            StartDate  = DateTime.Parse("2019-09-15")
                        },
                        Status = "finalised"
                    }
                });

            this._datacomService
                .Setup(x => x.GetTimesheets(It.IsAny<int>()))
                .ReturnsAsync(new List<Timesheet>
                {
                    new Timesheet
                    {
                        Id = "1234567890",
                        PayrunId = "123456789",
                        Status = "finalised",
                        Name = "Test",
                        Type = "API",
                        Values = new List<TimesheetValues>
                        {
                            new TimesheetValues
                            {
                                EmployeeId = "123",
                                Id = "1234567890",
                                StartDate  =  DateTime.Parse("2019-09-15"),
                                Value = 2.5m
                            },
                            new TimesheetValues
                            {
                                EmployeeId = "123",
                                Id = "1234567899",
                                StartDate  =  DateTime.Parse("2019-09-15"),
                                Value = 3m
                            }
                        }
                    }
                });
        }

        [Fact]
        public async Task Should_process_timesheet_sucess()
        {
            // Setup
            var companyCode = "TESTUnitTest";
            var startDate = DateTime.Parse("2019-08-05");
            var endDate = DateTime.Parse("2019-09-15");

            // Act
            var timesheetProcessor = new CsvTimesheetProcessor(this._datacomService.Object);
            var result = await timesheetProcessor.Process(companyCode, startDate, endDate);
            Assert.True(result);
        }

        [Fact]
        public async Task Should_process_timesheet_company_fail()
        {
            // Setup
            var companyCode = "TEST";
            var startDate = DateTime.Parse("2019-08-05");
            var endDate = DateTime.Parse("2019-09-15");

            // Act
            var timesheetProcessor = new CsvTimesheetProcessor(this._datacomService.Object);
            Task act() => timesheetProcessor.Process(companyCode, startDate, endDate);
            var exception =  await Assert.ThrowsAsync<TimesheetProcessorException>(act);
            Assert.Equal(exception.Message, $"Company code:{companyCode} not available");
        }

        [Theory]
        [InlineData("2019-05-05", "2019-08-01")]
        [InlineData("2019-05-05", "2019-06-05")]
        [InlineData("2019-05-05", "2019-07-05")]
        public async Task Should_process_timesheet_nodata_fail(string start, string end)
        {
            // Setup
            var companyCode = "TESTUnitTest";
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);

            // Act
            var timesheetProcessor = new CsvTimesheetProcessor(this._datacomService.Object);
            var result = await timesheetProcessor.Process(companyCode, startDate, endDate);
            Assert.False(result);
        }
    }
}
