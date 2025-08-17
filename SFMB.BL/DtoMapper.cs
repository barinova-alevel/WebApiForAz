using SFMB.BL.Dtos;
using SFMB.DAL.Entities;

namespace SFMB.BL
{
    public class DtoMapper
    {
        public OperationDto ToDto(Operation op)
        {
            return new OperationDto
            {
                OperationId = op.OperationId,
                Date = op.Date,
                Amount = op.Amount,
                Note = op.Note,
                OperationTypeId = op.OperationTypeId,
                OperationType = op.OperationType == null ? null : new OperationTypeDto
                {
                    OperationTypeId = op.OperationType.OperationTypeId,
                    Name = op.OperationType.Name,
                    Description = op.OperationType.Description,
                    IsIncome = op.OperationType.IsIncome
                }
            };
        }

        public DailyReportDto DailyReportToDto(DailyReport report)
        {
            return new DailyReportDto
            {
                Date = report.Date,
                TotalIncome = report.TotalIncome,
                TotalExpenses = report.TotalExpenses,
                Operations = report.Operations.Select(ToDto).ToList()
            };
        }

        public PeriodReportDto PeriodReportToDto(PeriodReport report)
        {
            return new PeriodReportDto
            {
                StartDate = report.StartDate,
                EndDate = report.EndDate,
                TotalIncome = report.TotalIncome,
                TotalExpenses = report.TotalExpenses,
                Operations = report.Operations.Select(ToDto).ToList()
            };
        }
    }
}
