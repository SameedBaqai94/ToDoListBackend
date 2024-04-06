using ToDoListBackend.Models;

namespace ToDoListBackend.Interfaces;

public interface IItemsRepository
{
    Task<ICollection<Items>> GetItems();
    Task<ICollection<Items>> GetItemsByListId(int listId);
    Task<bool> AddItems(Items items);
    Task<bool> UpdateItems(int listId, int itemId, Items items);
    Task<bool> RemoveItem(int itemId);
    Task<bool> ItemExists(int ItemId);
}