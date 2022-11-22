namespace ToDoList.Models
{
    public class todoList
    {
        public Guid Id { get; set; }

        public string? Task { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
