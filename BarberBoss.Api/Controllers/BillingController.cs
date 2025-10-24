using BarberBoss.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BarberBoss.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    private readonly IBillingService _billingService;

    public BillingController(IBillingService billigService)
    {
        _billingService = billigService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BillingResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBilling([FromBody] CreateBillingDto dto)
    {
        try
        {
            var newBilling = await _billingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetBillingById), new { id = newBilling.Id }, newBilling);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(BillingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBillingById(Guid id)
    {
        var billing = await _billingService.GetByIdAsync(id);

        if (billing is null)
        {
            return NotFound();
        }

        return Ok(billing);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BillingResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllBillings()
    {
        var billings = await _billingService.GetAllAsync();

        return Ok(billings);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(BillingResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBilling(Guid id, [FromBody] UpdateBillingDto dto)
    {
        try
        {
            var updateBilling = await _billingService.UpdateAsync(id, dto);
            if (updateBilling is null)
            {
                return NotFound();
            }

            return Ok(updateBilling);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var sucess = await _billingService.DeleteAsync(id);

        if (!sucess)
        {
            return NotFound();
        }

        return NoContent();
    }

}