namespace BarberBossManagement.Application.UseCases.Revenues.Reports.Excel;
public interface IGenerateRevenuesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly startDate);
}
