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
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string FinishDate { get; set; }
        public State State { get; set; }
        public Employee Assigned { get; set; }
        public Employee Created { get; set; }
        public Department Department { get; set; }
    }
}
