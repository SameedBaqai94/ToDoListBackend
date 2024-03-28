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
}