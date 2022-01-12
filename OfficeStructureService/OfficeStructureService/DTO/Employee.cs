namespace OfficeStructureService.DTO.Employee
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Department
    {
        public string Guid { get; set; }
        public string Name { get; set; }
    }

    public class Employee
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
