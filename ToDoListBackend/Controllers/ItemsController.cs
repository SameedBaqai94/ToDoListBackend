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
        var items = _mapper.Map<ICollection<ItemsReadDto>>(await _itemsRepository.GetItems());
        return Ok(items);
    }

    [HttpGet("GetItemsByListId")]
    public async Task<IActionResult> GetItemsByListId([FromQuery] int listId)
    {
        var items = _mapper.Map<ICollection<ItemsReadDto>>(await _itemsRepository.GetItemsByListId(listId));
        return Ok(items);
    }

    [HttpPost("AddItem")]
    public async Task<IActionResult> AddItem([FromQuery] int todolistid, [FromBody] ItemsCreateOrUpdateDto createItem)
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

    [HttpPut("UpdateItem")]
    public async Task<IActionResult> UpdateItem([FromQuery] int listId, [FromQuery] int itemId, [FromBody] ItemsCreateOrUpdateDto items)
    {
        if (items == null)
        {
            return NotFound(ModelState);
        }
        if (!await _listRepository.ListExists(listId) || !await _itemsRepository.ItemExists(itemId))
        {
            return BadRequest(ModelState);
        }

        var item = _mapper.Map<Items>(items);
        if (!await _itemsRepository.UpdateItems(listId, itemId, item))
        {
            ModelState.AddModelError("", "Something went wrong updating Item");
            return StatusCode(500, ModelState);
        }
        return StatusCode(200, "Item updated");
    }

    [HttpDelete("DeleteItem")]
    public async Task<IActionResult> DeleteItem([FromQuery] int itemId)
    {
        if (itemId == 0)
        {
            ModelState.AddModelError("", "Invalid Id");
            return BadRequest(ModelState);
        }
        if (!await _itemsRepository.ItemExists(itemId))
        {
            ModelState.AddModelError("", "Item not found");
            return NotFound(ModelState);
        }
        if (!await _itemsRepository.RemoveItem(itemId))
        {
            ModelState.AddModelError("", "Something went wrong removing Item");
            return StatusCode(500, ModelState);
        }
        return StatusCode(200, "Item removed");
    }
}