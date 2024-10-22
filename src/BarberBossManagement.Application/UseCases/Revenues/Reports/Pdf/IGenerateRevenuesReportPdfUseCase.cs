namespace BarberBossManagement.Application.UseCases.Revenues.Reports.Pdf;
public interface IGenerateRevenuesReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
