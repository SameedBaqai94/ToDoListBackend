namespace ToDoListBackend.Models;

public class Items
{
    public int ItemsId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }

    public int ToDoListId { get; set; }
    public ToDoList ToDoList { get; set; }
    
}