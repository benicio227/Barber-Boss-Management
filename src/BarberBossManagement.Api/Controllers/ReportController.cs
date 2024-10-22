using BarberBossManagement.Application.UseCases.Revenues.Reports.Excel;
using BarberBossManagement.Application.UseCases.Revenues.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace BarberBossManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateRevenuesReportExcelUseCase useCase,
        [FromHeader] DateOnly startDate)
    {
        byte[] file = await useCase.Execute(startDate);
        
        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
    [FromServices] IGenerateRevenuesReportPdfUseCase useCase,
    [FromQuery] DateOnly month)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
