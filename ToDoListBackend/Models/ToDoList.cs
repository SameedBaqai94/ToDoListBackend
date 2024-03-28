namespace ToDoListBackend.Models;

public class ToDoList
{
    public int ToDoListId { get; set; }
    public string Title { get; set; }
    public ICollection<Items> Items { get; set; }
}