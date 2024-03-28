using AutoMapper;
using ToDoListBackend.Dto;
using ToDoListBackend.Models;

namespace ToDoListBackend.Helpers;

public class MappingDto:Profile
{
    public MappingDto()
    {
        CreateMap<ItemsDto, Items>();
        CreateMap<Items, ItemsDto>();
        CreateMap<ToDoListDto, ToDoList>();
        CreateMap<ToDoList, ToDoListDto>();
        
    }
}