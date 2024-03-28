using ToDoListBackend.Models;

namespace ToDoListBackend.Interfaces;

public interface IItemsRepository
{
    Task<ICollection<Items>> GetItems();
    Task<bool> AddItems(Items items);
}