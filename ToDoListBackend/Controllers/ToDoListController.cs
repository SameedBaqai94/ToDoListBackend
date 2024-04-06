using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoListBackend.Dto;
using ToDoListBackend.Interfaces;
using ToDoListBackend.Models;

namespace ToDoListBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
   private readonly IToDoListRepository _listRepository;
   private readonly IItemsRepository _itemsRepository;
   private readonly IMapper _mapper;

   public ToDoListController(IToDoListRepository listRepository, IItemsRepository itemsRepository, IMapper mapper)
   {
      _listRepository = listRepository;
      _itemsRepository = itemsRepository;
      _mapper = mapper;
   }

   [HttpGet("GetList")]
   public async Task<IActionResult> GetList()
   {
      var lists = _mapper.Map<ICollection<ToDoListReadDto>>(await _listRepository.GetLists());
      return Ok(lists);
   }

   [HttpGet("GetListId")]
   public async Task<IActionResult> GetListId([FromQuery] string title)
   {
      var id = await _listRepository.GetListIdByTitle(title);
      return Ok(new { id = id });
   }

   [HttpPost("AddList")]
   public async Task<IActionResult> AddList([FromBody] ToDoListCreateOrUpdateDto createList)
   {

      var listMap = _mapper.Map<ToDoList>(createList);

      if (await _listRepository.AddList(listMap) == false)
      {
         ModelState.AddModelError("", "Something went wrong");
         return StatusCode(400, ModelState);
      }

      return StatusCode(200, "list added");
   }

   [HttpDelete("DeleteList/{id}")]
   public async Task<IActionResult> DeleteList(int id)
   {
      if (await _listRepository.ListExists(id) == false)
      {
         return NotFound();
      }

      if (!await _listRepository.DeleteList(id))
      {
         return StatusCode(400, "Something went wrong");
      }
      return StatusCode(200, "List deleted");
   }

   [HttpPut("UpdateList/{id}")]
   public async Task<IActionResult> UpdateList(int id, [FromBody] ToDoListCreateOrUpdateDto toDoListDto)
   {
      if (!await _listRepository.ListExists(id))
      {
         return NotFound();
      }
      if (toDoListDto == null)
      {
         return BadRequest();
      }
      var list = _mapper.Map<ToDoList>(toDoListDto);

      if (!await _listRepository.UpdateList(id, list))
      {
         ModelState.AddModelError("", "Something went wrong updating list");
         return StatusCode(500, ModelState);
      }
      return StatusCode(200, "List updated");

   }
}