using BarberBoss.Application.Interfaces.Reports;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [Route("excel")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetExcelReport([FromQuery] int month, [FromQuery] int year)
    {
        if (month < 1 || month > 12)
        {
            return BadRequest("O parâmetro 'month' (mês) deve estar entre 1 e 12.");
        }
        if (year < 2020 || year > 2100)
        {
            return BadRequest("O parâmetro 'year' (ano) deve ser um valor válido (ex: 2025).");
        }

        DateOnly startDate = new DateOnly(year, month, 1);
        DateOnly endDate = startDate.AddMonths(1).AddDays(-1);

        try
        {
            var fileBytes = await _reportService.GenerateExcelReportAsync(startDate, endDate);

            string fileName = $"Faturamento_BarberBoss_{year}-{month:00}.xlsx";

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno ao gerar relatório: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("pdf")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPdfReport([FromQuery] int month, [FromQuery] int year)
    {
        if(month < 1 || month > 12)
        {
            return BadRequest("O parâmetro 'month' (mês) deve estar entre 1 e 12.");
        }
        if (year < 2020 || year > 2100)
        {
            return BadRequest("O parâmetro 'year' (ano) deve ser um valor válido (ex: 2025)");
        }

        DateOnly startDate = new DateOnly(year, month, 1);
        DateOnly endDate = startDate.AddMonths(1).AddDays(-1);

        try
        {
            var fileBytes = await _reportService.GeneratePdfReportAsync(startDate, endDate);

            string fileName = $"Faturmento_BarberBoss_{year}-{month:00}.pdf";

            return File(fileBytes,
                "application/pdf",
                fileName);
        }
        catch (Exception ex)
        {
            return StatusCode((500), $"Erro interno ao gerar o relatório: {ex.Message}"); 
        }
    }
}
