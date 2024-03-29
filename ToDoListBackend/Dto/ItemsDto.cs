namespace ToDoListBackend.Dto;


public class ItemsCreateOrUpdateDto
{
    public string Description { get; set; }
    public string Status { get; set; }
}
public class ItemsReadDto
{
    public int ItemsId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}