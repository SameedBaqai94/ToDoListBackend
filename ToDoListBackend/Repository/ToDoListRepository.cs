using Microsoft.EntityFrameworkCore;
using ToDoListBackend.Data;
using ToDoListBackend.Interfaces;
using ToDoListBackend.Models;

namespace ToDoListBackend.Repository;

public class ToDoListRepository : IToDoListRepository
{
    private readonly DataContext _dataContext;

    public ToDoListRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task<ICollection<ToDoList>> GetLists()
    {
        return await _dataContext.ToDoList.Include(l => l.Items).ToListAsync();
    }

    public async Task<bool> AddList(ToDoList toDoList)
    {
        if (await _dataContext.ToDoList.AnyAsync(l => l.ToDoListId == toDoList.ToDoListId))
        {
            return false;
        }
        await _dataContext.ToDoList.AddRangeAsync(toDoList);
        await _dataContext.SaveChangesAsync();

        return true;
    }

    public async Task<ToDoList> GetList(int listId)
    {
        var list = await _dataContext.ToDoList.Where(l => l.ToDoListId == listId).FirstOrDefaultAsync();
        return list;
    }

    public async Task<bool> DeleteList(int listId)
    {
        var list = await _dataContext.ToDoList.FirstOrDefaultAsync(l => l.ToDoListId == listId);
        if (list != null)
        {
            _dataContext.ToDoList.Remove(list);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        return false;
    }


    public async Task<bool> ListExists(int listId)
    {
        if (!await _dataContext.ToDoList.AnyAsync(l => l.ToDoListId == listId))
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateList(int listId, ToDoList toDoList)
    {
        var list = await _dataContext.ToDoList.FindAsync(listId);
        if (list == null)
        {
            return false;
        }
        list.Title = toDoList.Title;
        return await _dataContext.SaveChangesAsync() > 0 ? true : false;
    }

    public async Task<int> GetListIdByTitle(string title)
    {
        var list = await _dataContext.ToDoList.FirstOrDefaultAsync(i => i.Title == title);
        if (list == null)
        {
            return -1;
        }
        return list.ToDoListId;
    }
}