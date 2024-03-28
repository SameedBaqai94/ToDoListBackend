using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoListBackend.Dto;
using ToDoListBackend.Interfaces;
using ToDoListBackend.Models;

namespace ToDoListBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IToDoListRepository _listRepository;

    private readonly IItemsRepository _itemsRepository;
    private readonly IMapper _mapper;

    public ItemsController(IItemsRepository itemsRepository, IToDoListRepository listRepository, IMapper mapper)
    {
        _itemsRepository = itemsRepository;
        _listRepository = listRepository;
        _mapper = mapper;
    }
    [HttpGet("GetItems")]
    public async Task<IActionResult> GetItems()
    {
        var items = _mapper.Map<ICollection<Items>>(await _itemsRepository.GetItems());
        return Ok(items);
    }


    [HttpPost("AddItems")]
    public async Task<IActionResult> AddItem([FromQuery] int todolistid, [FromBody] ItemsDto createItem)
    {
        var itemMap = _mapper.Map<Items>(createItem);
        itemMap.ToDoList = await _listRepository.GetList(todolistid);
        if (!await _itemsRepository.AddItems(itemMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }
        return Ok("Successfully created");
    }
}