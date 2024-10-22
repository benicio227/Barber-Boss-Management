using BarberBossManagement.Application.UseCases.Revenues.Reports.Pdf.Colors;
using BarberBossManagement.Application.UseCases.Revenues.Reports.Pdf.Fonts;
using BarberBossManagement.Domain.Extensions;
using BarberBossManagement.Domain.Reports;
using BarberBossManagement.Domain.Repositories.Revenues;
using MigraDoc.DocumentObjectModel;


using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace BarberBossManagement.Application.UseCases.Revenues.Reports.Pdf;
public class GenerateRevenuesReportPdfUseCase : IGenerateRevenuesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_REVENUE_TABLE = 25;
    private readonly IRevenuesReadOnlyRepository _repository;

    public GenerateRevenuesReportPdfUseCase(IRevenuesReadOnlyRepository repository)
    {
        _repository = repository;

        GlobalFontSettings.FontResolver = new RevenuesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly month)
    {
        var revenues = await _repository.FilterByMonth(month);

        if (revenues.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);
        var totalRevenues = revenues.Sum(revenues => revenues.Amount);
        CreateTotalSpentSection(page, month, totalRevenues);


        foreach (var revenue in revenues)
        {
            var table = CreateRevenueTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_REVENUE_TABLE;

            AddRevenueTitle(row.Cells[0], revenue.Title);
            AddHeaderForValue(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_REVENUE_TABLE;


            row.Cells[0].AddParagraph(revenue.Date.ToString("D"));
            SetStyleBaseForRevenueInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(revenue.Date.ToString("t"));
            SetStyleBaseForRevenueInformation(row.Cells[1]);
            row.Cells[2].AddParagraph(revenue.PaymentType.PaymentTypeToString());
            SetStyleBaseForRevenueInformation(row.Cells[2]);
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            AddAmountForRevenue(row.Cells[3], revenue.Amount);

            if (string.IsNullOrWhiteSpace(revenue.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_REVENUE_TABLE;

                descriptionRow.Cells[0].AddParagraph(revenue.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 10, Color = ColorsHelper.BLACK };
                descriptionRow.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGTH;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);

        }


        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportGenerationMessages.REVENUES_FOR} {month:Y}";
        document.Info.Author = "Benício Brandão";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;


        return section;
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");


        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "20240914_183326.jpg");
        
        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Hey, Benício Brandão");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalRevenues)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText();

        paragraph.AddFormattedText($"{totalRevenues} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateRevenueTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center; 
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center; 
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        return table;
    }

    private void AddRevenueTitle(Cell cell, string revenueTitle)
    {
        cell.AddParagraph(revenueTitle);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.RED_LIGTH;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForValue(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessages.AMOUNT);
        cell.Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForRevenueInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12, Color = ColorsHelper.BLACK };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForRevenue(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{amount} {CURRENCY_SYMBOL}");
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14, Color = ColorsHelper.WHITE };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();

        File.WriteAllBytes("ReveneueReport.pdf", file.ToArray());
        //renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
