using AutoMapper;
using ToDoListBackend.Dto;
using ToDoListBackend.Models;

namespace ToDoListBackend.Helpers;

public class MappingDto : Profile
{
    public MappingDto()
    {
        CreateMap<Items, ItemsReadDto>();
        CreateMap<ItemsCreateOrUpdateDto, Items>();
        CreateMap<ToDoList, ToDoListIdDto>();
        CreateMap<ToDoList, ToDoListReadDto>();
        CreateMap<ToDoListCreateOrUpdateDto, ToDoList>();

    }
}