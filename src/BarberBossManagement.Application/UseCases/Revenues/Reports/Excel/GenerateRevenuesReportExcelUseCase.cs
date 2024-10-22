using BarberBossManagement.Domain.Extensions;
using BarberBossManagement.Domain.Reports;
using BarberBossManagement.Domain.Repositories.Revenues;
using ClosedXML.Excel;

namespace BarberBossManagement.Application.UseCases.Revenues.Reports.Excel;
public class GenerateRevenuesReportExcelUseCase : IGenerateRevenuesReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private IRevenuesReadOnlyRepository _repository;

    public GenerateRevenuesReportExcelUseCase(IRevenuesReadOnlyRepository repository)
    {
        _repository = repository;
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var revenues = await _repository.FilterByMonth(month);

        if (revenues.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook(); // É como se tivessemos o arquivo excel em branco

        workbook.Author = "Benício Brandão";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y")); // Adiciona uma página no arquivo excel

        InsertHeader(worksheet);

        var row = 2;
        decimal totalRevenue = 0;

        foreach (var revenue in revenues)
        {
            worksheet.Cell($"A{row}").Value = revenue.Title;
            worksheet.Cell($"B{row}").Value = revenue.Date;
            worksheet.Cell($"C{row}").Value = revenue.PaymentType.PaymentTypeToString();


            worksheet.Cell($"D{row}").Value = revenue.Amount;
            worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";


            worksheet.Cell($"E{row}").Value = revenue.Description;

            totalRevenue += revenue.Amount;

            row++;
        }

        worksheet.Columns().AdjustToContents();

        worksheet.Cell($"C{row}").Value = "Total Revenue"; // Coloca o texto "Total Revenue"
        worksheet.Cell($"C{row}").Style.Font.Bold = true;

        worksheet.Cell($"D{row}").Value = totalRevenue; // Coloca o valor total do faturamento
        worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00"; // Formata como moeda
        worksheet.Cell($"D{row}").Style.Font.Bold = true;


        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray(); //Devolve um array de bytes, ou seja, cada posição organizada que representa o excel
    }

    

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);

    }
}
