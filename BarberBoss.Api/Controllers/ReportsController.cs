using BarberBoss.Application;
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
    public async Task<IActionResult> GetExcelReport([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("A date inicial não pode ser maior que a data final!");
        }

        try
        {
            var fileBytes = await _reportService.GenerateWeeklyExcelReportAsync(startDate, endDate);

            string fileName = $"Faturamento_BarberBoss_{startDate:dd-MM-yyyy}_ate_{endDate:dd-MM-yyyy}.xlsx";

            return File(fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Errointerno ao gerar relatório: {ex.Message}");
        }
    }
}
