using System;

namespace ServiceDesk.DTO.Task
{
    public enum State
    {
        Open,
        InProgress,
        Fixed,
        Closed
    }

    public class Employee
    {
        public string Guid { get; set; }
        public string Name { get; set; }
    }
    
    public class Department
    {
        public string Guid { get; set; }
        public string Name { get; set; }
    }

    public class Task
    {
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishDate { get; set; }
        public State State { get; set; }
        public Employee Assigned { get; set; }
        public Employee Created { get; set; }
        public Department Department { get; set; }
    }

    public class TaskState
    {
        public string Guid { get; set; }
        public State State { get; set; }
    }
}
