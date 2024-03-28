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
      var lists = _mapper.Map<ICollection<ToDoListDto>>(await _listRepository.GetLists());
      return Ok(lists);
   }

   [HttpPost("AddList")]
   public async Task<IActionResult> AddList([FromBody] ToDoListDto createList)
   {

      var listMap = _mapper.Map<ToDoList>(createList);

      if (await _listRepository.AddList(listMap) == false)
      {
         ModelState.AddModelError("", "Something went wrong");
         return StatusCode(400, ModelState);
      }

      return StatusCode(200, "list added");
   }
}