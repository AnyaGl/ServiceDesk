namespace ServiceDesk.Model
{
    public enum State
    {
        Open,
        InProgress,
        Fixed,
        Closed
    }
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public int AssignedId { get; set; }
        public int CreatedId { get; set; }
        public int DepartmentId { get; set; }
    }
}
