using ToDoListBackend.Models;

namespace ToDoListBackend.Interfaces;

public interface IToDoListRepository
{
    Task<ICollection<ToDoList>> GetLists();
    Task<ToDoList> GetList(int ListId);
    Task<bool> AddList(ToDoList toDoList);
}