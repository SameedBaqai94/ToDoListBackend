using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using ToDoListBackend.Data;
using ToDoListBackend.Interfaces;
using ToDoListBackend.Models;

namespace ToDoListBackend.Repository;

public class ItemsRepository : IItemsRepository
{
    private readonly DataContext _dataContext;



    public ItemsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<bool> AddItems(Items items)
    {
        await _dataContext.Items.AddAsync(items);
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<Items>> GetItems()
    {
        return await _dataContext.Items.ToListAsync();
    }

    public async Task<ICollection<Items>> GetItemsByListId(int listId)
    {
        var items = await _dataContext.Items.Where(i => i.ToDoListId == listId).ToListAsync();
        return items;
    }

    public async Task<bool> ItemExists(int ItemId)
    {
        if (!await _dataContext.Items.AnyAsync(i => i.ItemsId == ItemId))
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveItem(int itemId)
    {
        var item = await _dataContext.Items.FirstOrDefaultAsync(i => i.ItemsId == itemId);
        _dataContext.Items.Remove(item);
        return await _dataContext.SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<bool> UpdateItems(int listId, int itemId, Items items)
    {
        var item = await _dataContext.Items.FindAsync(itemId);
        if (items.Description != null && items.Description != item.Description)
        {
            item.Description = items.Description;
        }
        if (items.Status != null && items.Status != item.Status)
        {
            item.Status = items.Status;
        }

        return await _dataContext.SaveChangesAsync() > 0 ? true : false;
    }
}