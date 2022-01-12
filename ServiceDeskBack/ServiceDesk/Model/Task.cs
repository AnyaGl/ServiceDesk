using System;

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
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string FinishDate { get; set; }
        public State State { get; set; }
        public string AssignedId { get; set; }
        public string CreatedId { get; set; }
        public string DepartmentId { get; set; }
    }
}
