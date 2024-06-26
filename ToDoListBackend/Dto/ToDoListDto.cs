namespace ToDoListBackend.Dto;

public class ToDoListIdDto
{
    public int ToDoListId { get; set; }
}


public class ToDoListCreateOrUpdateDto
{
    // public int ToDoListId { get; set; }
    public string Title { get; set; }
}
public class ToDoListReadDto
{
    public int ToDoListId { get; set; }
    public string Title { get; set; }
    public ICollection<ItemsReadDto> Items { get; set; }
}