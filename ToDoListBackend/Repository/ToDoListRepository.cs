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

    public async Task<ToDoList> GetList(int ListId)
    {
        var list = await _dataContext.ToDoList.Where(l => l.ToDoListId == ListId).Include(l => l.Items).FirstOrDefaultAsync();
        return list;
    }
}