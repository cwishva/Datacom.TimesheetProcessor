using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public record TimeSheetModel
    {
        public int PayRunId { get; init; }
        public string EmployeeId { get; init; }
        public DateTime StartTime { get; init; }
        public decimal Sum { get; init; }
    }
}
