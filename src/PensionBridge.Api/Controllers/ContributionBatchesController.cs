using Microsoft.AspNetCore.Mvc;
using PensionBridge.Application.Ingestion;

namespace PensionBridge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContributionBatchesController : ControllerBase
{
    private readonly IContributionIngestionService _ingestionService;

    public ContributionBatchesController(IContributionIngestionService ingestionService)
    {
        _ingestionService = ingestionService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] Guid employerId, [FromForm] Guid schemeId, [FromForm] string period)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is required");
        }

        using var stream = file.OpenReadStream();
        var batch = await _ingestionService.IngestCsvAsync(employerId, schemeId, period, stream, file.FileName);
        
        return Ok(new { batchId = batch.Id, status = batch.Status });
    }
}
