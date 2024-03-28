using Microsoft.EntityFrameworkCore;
using ToDoListBackend.Models;

namespace ToDoListBackend.Data;

public class DataContext:DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<ToDoList> ToDoList { get; set; }
    public DbSet<Items> Items { get; set; }
}