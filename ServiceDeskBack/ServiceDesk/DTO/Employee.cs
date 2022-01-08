namespace ServiceDesk.DTO.Employee
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
    }
}
