using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class API
    {
        public static class Company
        {
            public const string Get_Companies = "v/1/companies";
        }

        public static class Pay
        {
            public const string Get_Paygroups = "v/1/paygroups";
            public const string Get_Payruns = "v/1/payruns";
        }

        public static class Timesheet
        {
            public const string Get_Timesheets = "v/2/timesheets?type=API";
        }

    }
}
