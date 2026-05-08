using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemReader _reader;
    private readonly IItemService _service;
    private readonly ILogger<ItemController> _logger;
    

    public ItemController(IItemReader reader,IItemService service, ILogger<ItemController> logger)
    {
        _reader = reader;
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("GET api/item called");
        //Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item called");

        var items = (await _reader.GetAllAsync()).ToList();
        var totalCount = items.Count;
        var averageValue = items.Any() ? items.Average(i => i.Value) : 0;
        //var itemList = items.ToList();

        //var totalCount = itemList.Count;
        //var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;
        //Console.WriteLine($"[LOG] Returning {totalCount} items, average value: {averageValue}");

        _logger.LogInformation("Returning {Count} items, average value: {AverageValue}", items.Count, items.Any() ? items.Average(i => i.Value) : 0);
        
        
        return Ok(new
        {
            Data = items,
            Statistics = new {
                TotalCount = totalCount,
                AverageValue = averageValue,
                RetrievedAt = DateTime.UtcNow
            }

        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("GET api/item/{Id} called", id);
        //Console.WriteLine($"[LOG] {DateTime.UtcNow}: GET api/item/{id} called");

        //if (id <= 0)
        //{
        //    Console.WriteLine($"[LOG] Invalid id: {id}");
        //    return BadRequest("Id must be a positive integer.");
        //}

        var item = await _reader.GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogWarning("Item with Id {Id} not found", id);
            //Console.WriteLine($"[LOG] Item {id} not found");
            return NotFound($"Item with Id {id} was not found.");
        }

        return Ok(item);
    }
}
