using ToDoListBackend.Models;

namespace ToDoListBackend.Interfaces;

public interface IToDoListRepository
{
    Task<ICollection<ToDoList>> GetLists();
    Task<ToDoList> GetList(int listId);
    Task<int> GetListIdByTitle(string title);
    Task<bool> AddList(ToDoList toDoList);

    Task<bool> DeleteList(int listId);
    Task<bool> UpdateList(int listId, ToDoList toDoList);
    Task<bool> ListExists(int listId);
}